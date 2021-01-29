var g_data,g_tree_data;
$(function () {
    // LoadTable();
    $("#editContainer").hide()
    InitTree();
    $.ajax({
        url: 'cat.aspx',
        type: "get",
    })

});

function GetTreeData(data) {

    var resultList = []
    var catList = _.filter(data, function (o) {

        return !o.sCat && !o.ssCat;
    });


    for (var i in catList) {
        var o = catList[i];

        var temp = {}
        temp["text"] = o.cat;
        temp["cat"] = o.cat;
        temp["scat"] = o.sCat;
        temp["sscat"] = o.ssCat;

        temp["oldCat"] = o.cat;
        temp["oldSCat"] = o.sCat;
        temp["oldSSCat"] = o.ssCat;


        temp["id"] = o.id - 1;
        temp["level"] = "0";
        temp["parent"] = "#";
        temp["isLeaf"] = true;
        temp["expanded"] = true;
        temp["loaded"] = true;
        resultList.push(temp);
    }

    var scatList = _.filter(data, function (o) {
        return !o.ssCat && o.sCat
    })

    for (var i in scatList) {
        var o = scatList[i];
        var temp = {}
        temp["text"] = o.sCat;
        temp["cat"] = o.cat;
        temp["scat"] = o.sCat;
        temp["sscat"] = o.ssCat;
        temp["id"] = o.id - 1;
        temp["level"] = "1";
        var parent;
        for (var j in resultList) {
            if (resultList[j].text == o.cat && resultList[j].level == 0) {
                parent = resultList[j].id;
                resultList[j].isLeaf = false;
            }
        }
        temp["oldCat"] = o.cat;
        temp["oldSCat"] = o.sCat;
        temp["oldSSCat"] = o.ssCat;
        temp["parent"] = parent;
        temp["isLeaf"] = true;
        temp["expanded"] = true;
        temp["loaded"] = true;
        resultList.push(temp);
    }

    var sscatList = _.filter(data, function (o) {
        return o.ssCat && o.sCat
    })

    for (var i in sscatList) {
        var o = sscatList[i];
        var temp = {}
        temp["text"] = o.ssCat;
        temp["id"] = o.id - 1;
        temp["cat"] = o.cat;
        temp["scat"] = o.sCat;
        temp["sscat"] = o.ssCat;

        temp["level"] = "2";
        var parent;
        for (var j in resultList) {
            if (resultList[j].text == o.sCat && resultList[j].cat == o.cat && resultList[j].level == 1) {
                parent = resultList[j].id
                resultList[j].isLeaf = false;
            }
        }
        temp["oldCat"] = o.cat;
        temp["oldSCat"] = o.sCat;
        temp["oldSSCat"] = o.ssCat;
        temp["parent"] = parent;
        temp["isLeaf"] = true;
        temp["expanded"] = false;
        temp["loaded"] = true;
        resultList.push(temp);
    }

    return _.sortBy(resultList, function (o) {
        return o.id;
    });
}
function InitTree() {
     
    $.ajax({
        url: '/handler/categoryhandler.ashx',
        type: "get",
        success: function (res) {
            
            var data = GetTreeData(res);
        
            g_tree_data= GetTreeData(res);
            $('#catalog_tree').jstree({
                'core':{
                    'data':data
                }
            });
        }
    })
  
}
$('#catalog_tree').on("changed.jstree", function (e, data) {
    $("#editContainer").show()
    var node=data.node.original;
    
    $("input[name=oldCat]").val(node.cat);
        $("input[name=oldSCat]").val(node.scat);
        $("input[name=oldSSCat]").val(node.sscat);
        SubBtn(node.level,node.id);
        buildSelect(g_tree_data,node.cat,node.level);
        buildScatSelect(g_tree_data,node.scat,node.parent,node.level);
    if(node.isLeaf){
        $("#deleteBtn").show()
    }else{
        $("#deleteBtn").hide()
    }


    if(node.level==0){
        $("input[name=cat]").val(node.cat);
        $("input[name=scat]").val("");
        $("input[name=sscat]").val("");
        $("#sscatRow").hide();
        $("#scatRow").hide();
    }else if (node.level==1) {   
        $("#scatRow").show();
        $("input[name=cat]").val(node.cat);
        $("input[name=scat]").val(node.scat);
        $("input[name=sscat]").val("");
        $("#sscatRow").hide();
    }else if(node.level==2){
        $("#scatRow").show();
        $("#sscatRow").show();
        $("input[name=cat]").val(node.cat);
        $("input[name=scat]").val(node.scat);
        $("input[name=sscat]").val(node.sscat);
    }
  });

//   function validator()
//   {
//      var $form=$("#NewCategoryForm").serializeArray();
//      console.log($form);
//       if(confirm("确认要执行此操作吗？")==true)
//           return true;
//       else
//           return false;
//   }
  $(document).on("click","#deleteBtn",function(e){
    e.preventDefault();
    if(confirm("Are you sure to delete?")==true){
        var newElement = document.createElement("input");
        newElement.setAttribute("name","oper");
        newElement.setAttribute("value",'del');
        newElement.style.visibility="hidden";//设置为隐藏
        $("#NewCategoryForm").append(newElement);
        $("#NewCategoryForm").submit();
    }
  })
function SubBtn(level,id){
    $(".categoryBtn").remove();
    var button='';
  
    if(level==0){
        button="<button type=\"button\" class=\"btn btn-primary ml-4 categoryBtn\"  onclick=AddNewSCat("+id+") >Add New 2nd Category</button>";

    }else if(level==1){
        button="<button type=\"button\" class=\"btn btn-primary ml-4 categoryBtn\"  onclick=AddNewSSCat("+id+") >Add New 3rd Category</button>";

    }else if (level=2){
        button=''
    }
    $("#saveBtn").after(button);
}



function AddNewSCat(id) {
    var catObj=g_tree_data[id];
    $("#s_new_cat").val(catObj.cat)
    $("#scategoryModal").modal('show')
    
}
function AddNewSSCat(id) {
    var catObj=g_tree_data[id];
    $("#ss_new_cat").val(catObj.cat)
    $("#ss_new_scat").val(catObj.scat)
    $("#sscategoryModal").modal('show')
    
}
function buildSelect(data,value,level) {
if(level==0){
    $("#catDiv").html("<input type=\"text\" class=\"form-control\" name='cat'>")
    }else{
        var catList=[];
        for(var j=0;j<data.length;j++){
          //   console.log(data[j]);
            if(data[j].level==0){
              catList.push({cat:data[j].cat,id:data[j].id})
            }
        }
          var select=$("<select>",{class:"catSelect form-control",name:"cat"});
          for( var i in catList){
              var each=catList[i];
           
              if(each.cat==value){
                  $(select).append(`<option value='${each.cat}' data-id=${each.id} selected>${each.cat}</option>`);
              }else{
                  $(select).append(`<option value='${each.cat}' data-id=${each.id}>${each.cat}</option>`);
              }
          }
          $("#catDiv").html(select)
    }
  
    return select;
}

function buildScatSelect(data,value,parent,level){
    if(level==1){
        $("#scatDiv").html("<input type=\"text\" class=\"form-control\" name='scat'>")
    }else{
        var rootId;
        for(var i of data){
            if(i.id==parent){
                rootId=i.parent
            }
        }
    
    
        var select=$("<select>",{class:"scatSelect form-control",name:"scat"});
        for( var i in data){
            
            var each=data[i];
            if(each.parent==rootId){
              
                
                if(each.scat==value){
                    $(select).append(`<option value='${each.scat}' data-id=${each.id} selected>${each.scat}</option>`);
                }else{
                    $(select).append(`<option value='${each.scat}' data-id=${each.id}>${each.scat}</option>`);
                }
            }
        
        }
        $("#scatDiv").html(select)
    }


}

$(document).on("change",".catSelect",function (e) {
    var root_id=$(this).find("option:selected").data("id");
    
    $(".scatSelect").empty();
    var result="";
    for(var i in g_tree_data){
        console.log(g_tree_data[i].parent);
        if(g_tree_data[i].parent==root_id){
            result+="<option value='"+g_tree_data[i].scat+"' data_id="+g_tree_data[i].id+">"+g_tree_data[i].scat+"</option>"
        }
    }
    console.log(result);
    $(".scatSelect").append(result);
})

$(document).on("click","#UpdateCategoryBtn",function(e){
    $("#updateBtn").click();
})


$(document).on("click","#AddCategoryBtn",function(e){
        $("#categoryModal").modal('show')
})


