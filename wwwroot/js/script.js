$(document).ready(function() {
    $('#example').DataTable();


    $("#dpd-close").click(function() {
        $("#sec-details").hide(100);
    })

    $("#btn-download").click(function() {
        $("#sec-details").css("display", "block");


    })
    $("#btn-submit").click(function(e){
        var xml = $("#xml").val()
        var csv = $("#csv").val()
        var xls = $("#xls").val()
        if ($('#xml').is(":checked"))
        {
            window.location.href = "xml";
        }
        else if ($("#csv").is(":checked")) {
            window.location.href = "csv";
        }
        else if($("#xls").is(":checked")){
            window.location.href = "xls";
        }
        
        e.preventDefault();
    })
})