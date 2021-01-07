var glo_date = "today";
var glo_suffix = "today";
var barChart;
var pieChart;
var barChart2;
var pieChart2;
var barChart3;
var pieChart3;
var jsondata;
var $mytable;
var istable=false;

$(function () {

  getBranch();
  getjson();
  // $(".datefooter p").html(glo_date);

})



function getjson() {
  if(getCookieBranchId()!=1&&getCookieAccessLevel()<enduserLevel){
    glo_suffix = setQuery(glo_date, "branchid=" +getCookieBranchId());
    $(".allbranch").hide();
    $(".branchinfo").removeClass("hidden");
  }else{
    glo_suffix=glo_date;
  }
  var url = urlprefix + "api/dashboard/" + glo_suffix;

  $.ajax({
    type: "get",
    url: url,
    async: true,
    beforeSend:function(data){
      // document.getElementById("totalsales").innerHTML = "loading...";
      // document.getElementById("totalprofit").innerHTML = "loading...";
      // document.getElementById("totaltrans").innerHTML = "loading...";
      // document.getElementById("margin").innerHTML = "loading...";
  $("#loading").html("<img src='"+loadingpic+"'>");
  },

    success: function(data) {

      $("#loading").empty();
      jsondata = data;
  
     $('#branchDetail').bootstrapTable('destroy').bootstrapTable({
      striped: true,
      search: false,
      pagination: false,
      pageSize: 10,
      pageList: [10, 20, 50, 100],
      toggle:true,
      showRefresh: false,
      idField: 'id',
      dataType: 'jsonp',
      sortable:true,
      // height: $(window.innerHeight)[0]-100,

      data:data,
      columns:
      [  {
          field: 'branchName',
          title: 'Branch',
           align:"center",
           class:"Witemname",
    
        },
        {
          field: 'invoiceQuantity',
          title: 'Quantity',
          sortable:true,
          align: 'center',
    

        },

        {
          field: 'totalWithGST',
          title: 'Amount(IncGST)',
          align: 'center',
          sortable:true,
          formatter: function(value, row, index) {
              return turnToMoney(value.toFixed(2))
            },
  
        },
        {
            field: 'profitWithGST',
            title: 'Profit(IncGST)',
            align: 'center',
            sortable:true,
            formatter: function(value, row, index) {
                return turnToMoney(value.toFixed(2))
              },
          },
          {
            field: 'invoiceQuantity',
            title: 'Margin',
            align: 'center',
            sortable:true,
            formatter: function(value, row, index) {
              var mmargin=row.profitWithGST/row.totalWithGST*100;
                return mmargin.toFixed(2)+"%";
              },
          },
          {
            field: 'invoiceQuantity',
            title: 'Average Size',
            align: 'center',
            sortable:true,
            formatter: function(value, row, index) {
              var as=row.totalWithGST/value;
                return turnToMoney(as.toFixed(2))
              },
          }


      ]
    });
      getinfo();
    getBranchsale();
    },
    error: function() {
      console.log('error');
    }

  });
}



$('.datesel a').on('click', function(ev) {
  ev.preventDefault();
  var val = $(this).data("val");
  var items = $('.datesel').find('a');
  items.removeClass('active');
  $.each(items, function(i, item) {
    if ($(item).data("val") == val) {
      $(item).addClass('active');
      glo_date = val;
      var show;
      switch (glo_date) {
        case "thisweek":
            show="this week"
          break;
          case "thismonth":
              show="this month"
            break;
            case "today":
                show="today"
              break;
              case "yesterday":
                  show="yesterday"
                break;
      }
        $(".datefooter p").html(show);
    };
  });
  var mytab = $("#myTab .active").index();
  getjson();

});


function getinfo() {
  var totalsales = 0;
  var totalprofit = 0;
  var totaltrans = 0;
  var content = "";
  var bn="";
  var as=0;
  for (var i = 0; i < jsondata.length; i++) {

    totalsales = jsondata[i].totalWithGST + totalsales;
    totalprofit = jsondata[i].profitWithGST + totalprofit;
    totaltrans = jsondata[i].invoiceQuantity + totaltrans;
  }

  if(getCookieBranchId!=1){

    bn=branchDic[getCookieBranchId()];
    as=turnToMoney((totalsales/totaltrans).toFixed(2));
  }
  var result1 = turnToMoney(totalsales.toFixed(2));
  var result2 = turnToMoney(totalprofit.toFixed(2));
  var result3 = totaltrans;
  var result4 = (totalprofit / totalsales * 100).toFixed(2) + "%";
  document.getElementById("totalsales").innerHTML = result1;
  document.getElementById("totalprofit").innerHTML = result2;
  document.getElementById("totaltrans").innerHTML = result3;
  document.getElementById("margin").innerHTML = result4;
  document.getElementById("branchname").innerHTML = bn;
  document.getElementById("averagesize").innerHTML = as;
}

function getBranchsale() {
  var barData = [];
  var barLabel = [];
  var pieData = [];
  var barData2 = [];
  var pieData2 = [];
  var barData3 = [];
  var pieData3 = [];
  for (var i = 0; i < jsondata.length; i++) {
    barLabel[i] = jsondata[i].branchName;
    barData[i] = jsondata[i].totalWithGST;
    pieData[i] = {
      name: jsondata[i].branchName,
      value: jsondata[i].totalWithGST
    };
    barData2[i] = jsondata[i].profitWithGST ;
    pieData2[i] = {
      name: jsondata[i].branchName,
      value: jsondata[i].profitWithGST
    };
    barData3[i] = jsondata[i].invoiceQuantity;
    pieData3[i] = {
      name: jsondata[i].branchName,
      value: jsondata[i].invoiceQuantity
    };
  }
  if (barChart != null && barChart != "" && barChart != undefined) {
    barChart.dispose();
  }
  if (pieChart != null && pieChart != "" && pieChart != undefined) {
    pieChart.dispose();
  }
  if (barChart2 != null && barChart2 != "" && barChart2 != undefined) {
    barChart2.dispose();
  }
  if (pieChart2 != null && pieChart2 != "" && pieChart2 != undefined) {
    pieChart2.dispose();
  }
  if (barChart3 != null && barChart3 != "" && barChart3 != undefined) {
    barChart3.dispose();
  }
  if (pieChart3 != null && pieChart3 != "" && pieChart3 != undefined) {
    pieChart3.dispose();
  }
  barChart = echarts.init(document.getElementById('salesbar'));
  pieChart = echarts.init(document.getElementById('salespie'));
  barChart2 = echarts.init(document.getElementById('profitbar'));
  pieChart2 = echarts.init(document.getElementById('profitpie'));
  barChart3 = echarts.init(document.getElementById('transbar'));
  pieChart3 = echarts.init(document.getElementById('transpie'));
  option = {
    title: {
      text:getSubTitle(glo_date),
      x: 'center',
      textStyle: {
        color: '#777777',
        fontStyle: 'normal',
        fontWeight: 'bold',
        fontFamily: 'sans-serif',
        fontSize: 15
      }

    },
    tooltip: {
      confine: true,
      trigger: "axis",
      axisPointer: {
        type: 'shadow'
      },
      formatter: function(p) {
        var result = p[0].name + " : " + turnToMoney(p[0].data);
        return result;
      }
    },
    grid: {
  x:60,
  x2:10,
  bottom:90
      //y:
    },

    xAxis: {
      type: 'category',
      data: barLabel,
      axisLabel: {
        interval: 0,
        rotate: 40,
        margin: 10,
        color: "#777777",
        fontWeight: 'bold',
        fontSize: 12,
        formatter: function(p) {

            return splitLabel(p);
        }
      },


    },
    yAxis: {
      type: 'value',
      axisLabel:{
        formatter:function (p) {
          return  showNumber(p);
        }
      }
    },
    series: [{

      data: barData,
      barWidth: "20",

      type: 'bar',
      itemStyle: {
        normal: {
          color: 'rgba(43,137,204, 0.8)',
          barBorderColor: '#2b89cc'
        }
      },
      label: {
        normal: {
          show: false,
          position: 'top',
          color: "blue",
          formatter: function(params) {
            if (params.value > 0) {
              return turnToMoney(params.value);
            } else {
              return '';
            }
          }
        }
      }
    }]
  };
  option2 = {

    grid: {

      containLabel: true
    },

    series: [{
      hoverAnimation: false,
      name: "Sales",
      type: 'pie',
      radius: '100%',
      //center: ['50%', '50%'],
      data: pieData,
      itemStyle: {
        normal: {
          color: function(params) {
            var colorList = ["#df9499", "#2ec7c9", "#b6a2de", "#467190", "#ffb980", "#8CC9E8","#e26159","#734ba8","#2b89cc"];
                var i=params.dataIndex%colorList.length;
            return colorList[i];
          },
          label: {
            show: true,
            position: "inner",
            formatter: function(params) {

              var res = params.name + " : \n" + params.percent + "%";
              return res;
            }

          },
          labelLine: {
            show: true
          },
          emphasis: {
            shadowBlur: 10,
            shadowOffsetX: 0,
            shadowColor: 'rgba(0, 0, 0, 0.5)'
          }
        },

      }
    }]
  };
  option3 = {
    title: {
      text: getSubTitle(glo_date),
      x: 'center',
      textStyle: {
        color: '#777777',
        fontStyle: 'normal',
        fontWeight: 'bold',
        fontFamily: 'sans-serif',
        fontSize: 15
      }
    },
    tooltip: {
      confine: true,
      trigger: "axis",
      axisPointer: {
        type: 'shadow'
      },
      formatter: function(p) {
        var result = p[0].name + " : " + turnToMoney(p[0].data);
        return result;
      }
    },
    grid: {
      x:60,
      x2:10,
      bottom:90

      //y:
    },
    xAxis: {
      type: 'category',
      data: barLabel,
      axisLabel: {
        interval: 0,
        rotate: 40,
        margin: 10,
        color: "#777777",
        fontWeight: 'bold',
        fontSize: 12,
        formatter: function(p) {

            return splitLabel(p);
        }
      }

    },
    yAxis: {
      type: 'value',
      axisLabel:{
        formatter:function (p) {
          return  showNumber(p);
        }
      }

    },
    series: [{
      data: barData2,
      barWidth: "20",
      type: 'bar',
      itemStyle: {
        normal: {
          color: 'rgba(43,137,204, 0.8)',
          barBorderColor: '#2b89cc'
        }
      },
      label: {
        normal: {
          show: false,
          position: 'top',
          color: "blue",
          formatter: function(params) {
            if (params.value > 0) {
              return turnToMoney(params.value);
            } else {
              return '';
            }
          }
        }
      }
    }]
  };
  option4 = {

    grid: {
      containLabel: true
    },
    series: [{
      hoverAnimation: false,
      name: "Profit",
      type: 'pie',
      radius: '100%',
      center: ['50%', '50%'],
      data: pieData2,
      itemStyle: {
        normal: {
          color: function(params) {
            var colorList = ["#df9499", "#2ec7c9", "#b6a2de", "#467190", "#ffb980", "#8CC9E8","#e26159","#734ba8","#2b89cc"];
                var i=params.dataIndex%colorList.length;
            return colorList[i];
          },
          label: {
            show: true,
            position: "inner",
            formatter: function(params) {

              var res = params.name + " : \n" + params.percent + "%";
              return res;
            }
          },
          labelLine: {
            show: true
          },
          emphasis: {
            shadowBlur: 10,
            shadowOffsetX: 0,
            shadowColor: 'rgba(0, 0, 0, 0.5)'
          }
        },

      }
    }]
  };
  option5 = {
    title: {
      text:getSubTitle(glo_date),
      x: 'center',
      textStyle: {
        color: '#777777',
        fontStyle: 'normal',
        fontWeight: 'bold',
        fontFamily: 'sans-serif',
        fontSize: 15
      }
    },
    tooltip: {
      confine: true,
      trigger: "axis",
      axisPointer: {
        type: 'shadow'
      }
    },
    grid: {
      x:60,
      x2:10,
  bottom:90

      //y:
    },
    xAxis: {
      type: 'category',
      data: barLabel,
      axisLabel: {
        interval: 0,
        rotate: 40,
        margin: 10,
        color: "#777777",
        fontWeight: 'bold',
        fontSize: 12,
        formatter: function(p) {

            return splitLabel(p);
        }
      }

    },
    yAxis: {
      type: 'value',
      axisLabel:{
        formatter:function (p) {
          return  showNumber(p);
        }
      }
    },
    series: [{
      data: barData3,
      barWidth: "20",
      type: 'bar',
      itemStyle: {
        normal: {
          color: 'rgba(43,137,204, 0.8)',
          barBorderColor: '#2b89cc'
        }
      },
      label: {
        normal: {
          show: false,
          position: 'top',
          color: "blue",
          formatter: function(params) {
            if (params.value > 0) {
              return params.value;
            } else {
              return '';
            }
          }
        }
      }
    }]
  };
  option6 = {
      series: [{
        hoverAnimation: false,
        name: "Transactions",
        type: 'pie',
        radius: '100%',
        center: ['50%', '50%'],
        data: pieData3,
        itemStyle: {
          normal: {
            color: function(params) {
              var colorList = ["#df9499", "#2ec7c9", "#b6a2de", "#467190", "#ffb980", "#8CC9E8","#e26159","#734ba8","#2b89cc"];
                  var i=params.dataIndex%colorList.length;
              return colorList[i];
            },
            label: {
              show: true,
              position: "inner",
              formatter: function(params) {
                var res = params.name + " : \n" + params.percent + "%";
                return res;
              }
            },
            labelLine: {
              show: true,
              normal: {
                length: 100
              }
            },
            emphasis: {
              shadowBlur: 10,
              shadowOffsetX: 0,
              shadowColor: 'rgba(0, 0, 0, 0.5)'
            }
          },

        }
      }]
    };

  barChart.setOption(option);
  pieChart.setOption(option2);
  barChart2.setOption(option3);
  pieChart2.setOption(option4);
  barChart3.setOption(option5);
  pieChart3.setOption(option6);





  $(window).on('resize', function() {
    barChart.resize();
    pieChart.resize();
    barChart2.resize();
    pieChart2.resize();
    barChart3.resize();
    pieChart3.resize();
  });
}
