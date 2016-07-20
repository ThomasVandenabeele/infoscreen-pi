
$(document).ready(function() {

    $('#stateCheck').change(function() {
        
        var stateBool =  Boolean($(this).is(":checked"));
        console.log(stateBool);

        var itemId = parseInt((($(this).closest("tr").children("td"))[0]).innerHTML);
        console.log(itemId);

        var postData = { id: itemId, state: stateBool };

        $.ajax({
            type: "POST",
            url: '/Config/ChangeItemState',
            data: postData,
            success: function(response, textStatus, jqXHR){
                console.log(response);
                if(response.success){
                    alertify.success(response.message);
                }
                else{
                    alertify.error(response.message);
                }
            },
            dataType: "json",
            traditional: true
        });

    });

    var dialogInstance = new BootstrapDialog();
    $("#openArchive").click(function(){

        $.get("/Config/ItemsArchive", function (data) {

                dialogInstance
                    .setTitle('Item Archief')
                    .setMessage($('<div></div>').html(data))
                    .setSize(BootstrapDialog.SIZE_WIDE)
                    .setType(BootstrapDialog.TYPE_PRIMARY)
                    .open();
            });
    
    });

    $( "#nieuw" ).click(function() {
        alertify.confirm("Message", function (e) {
            if (e) {
                alertify.success("Success notification");
            } else {
                // user clicked "cancel"
            }
        });
    });

    $(document).on('click','.verwijder',function(){
        console.log((($(this).closest("tr").children("td"))[2]));
        var td = (($(this).closest("tr").children("td"))[2]);

        var itemTitle = $(td).children("b").html();

        var itemId = parseInt((($(this).closest("tr").children("td"))[0]).innerHTML);
        console.log(itemId);
        console.log(itemTitle);

        var postData = { id: itemId, state: Boolean(true) };

        alertify.confirm("Bent u zeker dat u '" + itemTitle + "' wilt archiveren?", function (e) {
            if (e) {
                $.ajax({
                    type: "POST",
                    url: '/Config/ArchiveItem',
                    data: postData,
                    success: function(response, textStatus, jqXHR){
                        console.log(response);
                        if(response.success){
                            $("#itemsTable").empty();

                            $.get("/Config/ItemsTable", function (data) {
                                $("#itemsTable").html(data);
                                $('input[type=checkbox]').each(function () {
                                    $(this).bootstrapToggle();
                                });
                            });
                            alertify.success(response.message);
                        }
                        else{
                            alertify.error(response.message);
                        }
                    },
                    dataType: "json",
                    traditional: true
                });
            } else {
                // user clicked "cancel"
            }
        });
    });

    $(document).on('click','.activateItem',function(){
        console.log((($(this).closest("tr").children("td"))[2]));
        var td = (($(this).closest("tr").children("td"))[2]);

        var itemTitle = $(td).children("b").html();

        var itemId = parseInt((($(this).closest("tr").children("td"))[0]).innerHTML);
        console.log(itemId);
        console.log(itemTitle);

        var postData = { id: itemId, state: Boolean(false) };

        alertify.confirm("Bent u zeker dat u '" + itemTitle + "' wilt activeren?", function (e) {
            if (e) {

                dialogInstance.close();
                $.ajax({
                    type: "POST",
                    url: '/Config/ArchiveItem',
                    data: postData,
                    success: function(response, textStatus, jqXHR){
                        console.log(response);
                        if(response.success){
                            $("#itemsTable").empty();

                            $.get("/Config/ItemsTable", function (data) {
                                $("#itemsTable").html(data);
                                $('input[type=checkbox]').each(function () {
                                    $(this).bootstrapToggle();
                                });
                            });
                            alertify.success(response.message);
                        }
                        else{
                            alertify.error(response.message);
                        }
                    },
                    dataType: "json",
                    traditional: true
                });
            } else {
                // user clicked "cancel"
            }
        });
    });
});

//------------------------------------------------------------------------------------------------------------------------------------//

$(document).ready(function() {

        $( "#TickerOpslaan" ).click(function() {
            var stringArray2 = $('#TickerData').val().split('\n');
            var postData = { listkey: stringArray2 };

            $.ajax({
                type: "POST",
                url: '/Config/SaveTicker',
                data: postData,
                success: function(response, textStatus, jqXHR){
                    console.log(response);
                    if(response.success){
                        alertify.success(response.message);
                    }
                    else{
                        alertify.error(response.message);
                    }
                },
                dataType: "json",
                traditional: true
            });

        });

    });