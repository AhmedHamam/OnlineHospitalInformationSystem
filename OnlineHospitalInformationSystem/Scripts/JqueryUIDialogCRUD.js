 $(document).ready(function () {
        $("td,th").css('text-align', 'center');
    
        var url = "";

        

      

       

        $("#dialog-detail").dialog({
            title: $("#DialogHeaderIdTextBox").val(),
            autoOpen: false,
            resizable: false,
            width:900,
            position:[170,60],
   
            modal: true,
            draggable: false,
            open: function (event, ui) {
                $(".ui-dialog-titlebar-close").hide();
                $(this).load(url);
            },
            buttons: {
                "Close": function () {
                    $(this).dialog("close");
                }
            }
        });



       

        $(".lnkDetail").live("click", function (e) {
            // e.preventDefault(); use this or return false
            url = $(this).attr('href');
            $("#dialog-detail").dialog('open');

            return false;
        });
   
        $("#btnClose").live("click", function (e) {
            $("#dialog-edit").dialog("close");
            return false;
        });
    });