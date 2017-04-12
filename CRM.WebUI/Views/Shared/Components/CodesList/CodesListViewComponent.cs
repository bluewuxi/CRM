using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using CRM.WebUI.Models;

namespace CRM.WebUI.ViewComponents
{
    public class CodesListViewComponent : ViewComponent
    {
        public static string Register (string formId, string callbackFunc)
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

        string Script;
        if (formId == "") formId = "CodesListModal";
        Script = "$(document).ready(function(){$(document).on('shown.bs.modal','#myModal',function(){$('#myModal .searchString').val(''),$('#myModal .searchString').focus(),LC_List_myModal('users')}),$('#myModal .searchString').on('input',LC_Debounce_myModal(function(){$('#myModal #codetable').bootstrapTable('refresh',{url:LC_GetUrl_myModal('users',this.value,1)})},500))}),$('#myModal').on('check.bs.table',function(){$('#myModal').modal('toggle'),yourCallback($('#myModal #codetable').bootstrapTable('getSelections'))});";
        return  Script.Replace("myModal",formId).Replace("yourCallback",callbackFunc);
        }


        public CodesListViewComponent()
        {
        }

        public IViewComponentResult Invoke(string codeName, string modalId)
        {
            return View(codeName, modalId);
        }
    }

}
