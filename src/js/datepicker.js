

$(function () {

    $("input[onclick='calendar()']").prop('autocomplete',"off");
})
/****加载css文件 */

//****calendar()方法 */
function calendar() {
 
    $(event.target).datepicker('destroy');

    $(event.target).datepicker({
        format: "dd-mm-yyyy", //初始格式
        todayBtn: "linked",//打开快速选择today选项
        autoclose:true,   
        orientation:'bottom', // calendar显示在input的下方
        autocomplete: 'off',
    });
    $(event.target).datepicker('show');      
}
