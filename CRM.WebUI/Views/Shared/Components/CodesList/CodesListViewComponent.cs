using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CRM.WebUI.ViewComponents
{
    [ViewComponent(Name = "CodesList")]
    public class CodesListViewComponent : ViewComponent
    {
        public static List<string> funcList= new List<string>();

        public static string Register() //(string formId, string callbackFunc)
        {
            //< script >
            //        $(document).ready(function() {
            //            $(document).on('shown.bs.modal', '#myModal', function() {
            //                $('#myModal .searchString').val("");
            //                $('#myModal .searchString').focus();
            //                LC_List_myModal('user');
            //            });
            //            $('#myModal .searchString').on('input', LC_Debounce_myModal(function() { $('#myModal #codetable').bootstrapTable('refresh', { url: LC_GetUrl_myModal('user', this.value, 1) }); }, 500));
            //        });
            //        $("#myModal").on('check.bs.table', function() {
            //            $('#myModal').modal('toggle');
            //            yourCallback($("#myModal #codetable").bootstrapTable('getSelections'))
            //        });

            //        function yourCallback(eSelect)
            //        {
            //            if (eSelect != null) {$("#display").val(eSelect[0].id);}
            //        }
            //</ script >

            //string Script;
            //if (formId == "") formId = "CodesListModal";
            //Script = "$(document).ready(function(){$(document).on('shown.bs.modal','#myModal',function(){$('#myModal .searchString').val(''),$('#myModal .searchString').focus(),LC_List_myModal('users')}),$('#myModal .searchString').on('input',LC_Debounce_myModal(function(){$('#myModal #codetable').bootstrapTable('refresh',{url:LC_GetUrl_myModal('users',this.value,1)})},500))}),$('#myModal').on('check.bs.table',function(){$('#myModal').modal('toggle'),yourCallback($('#myModal #codetable').bootstrapTable('getSelections'))});";
            //return  Script.Replace("myModal",formId).Replace("yourCallback",callbackFunc);
            string output = "<script>";
            foreach (string iList in funcList)
            {
                output += "boundButton_" + iList + "();";
                output += "$('#"+ iList + "Select').on('show.bs.modal',initTable_"+iList+"());";
            }
            output += "</script>";
            funcList.Clear();
            return output;
        }


        public IViewComponentResult Invoke(string crmCode, string valueRef, string displayRef, string buttonRef, string onSelectFunc)
        {
            funcList.Add(valueRef);
            return View(crmCode, new string[] { valueRef + "Select", valueRef, displayRef, buttonRef, onSelectFunc ?? "" });
        }

        //public async Task<IViewComponentResult> InvokeAsync(string crmCode, string valueRef, string displayRef, string buttonRef, string onSelectFunc)
        //{
        //    funcList.Add(valueRef);
        //    await Task.Delay(0);
        //    return View(crmCode, new string[] { valueRef + "Select", valueRef, displayRef, buttonRef, onSelectFunc ?? "" });
        //}

    }

}
