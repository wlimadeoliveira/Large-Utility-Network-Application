﻿var table1 = $('#datatable3').DataTable({
    'paging': true,
    'ordering': true,
    'info': true,
    "orderSequence": ["desc", "asc"],
    responsive: true,
    stateSave: false,

    oLanguage: {
        sSearch: 'Search all columns:',
        sLengthMenu: '_MENU_ records per page',
        info: 'Showing page _PAGE_ of _PAGES_',
        zeroRecords: 'Nothing found - sorry',
        infoEmpty: 'No records available',
        infoFiltered: '(filtered from _MAX_ total records)',
        oPaginate: {
            sNext: '<em class="fa fa-caret-right"></em>',
            sPrevious: '<em class="fa fa-caret-left"></em>'
        }
    },
    keys: true
});




$('#buttonhistory').click(function (evt) {
    evt.preventDefault();
    $('#historyaddedsuccess').html("");
    $.ajax({
        type: "POST",
        url: '/Product/QuickAddHistory',
        data: $('#formproducthistory').serialize(),
        dataType: 'application/json',
        async: false,
        cache: false,
        success: function (data, status) {
            alert("Hello");
        },
        error: function (data, status) {

        },
    });
    console.log($('#formproducthistory').serialize());
    $('#historyaddedsuccess').html("History was succesfully added");


    var url = '@Url.Action("HistoryTable", "Product", new { id = Model.ID})';

    table1.destroy();
    $("#historytable").load(url, function () {

        var table1 = $('#datatable3').DataTable({
            'paging': true,
            'ordering': true,
            'info': true,
            "orderSequence": ["desc", "asc"],
            responsive: true,
            stateSave: false,

            oLanguage: {
                sSearch: 'Search all columns:',
                sLengthMenu: '_MENU_ records per page',
                info: 'Showing page _PAGE_ of _PAGES_',
                zeroRecords: 'Nothing found - sorry',
                infoEmpty: 'No records available',
                infoFiltered: '(filtered from _MAX_ total records)',
                oPaginate: {
                    sNext: '<em class="fa fa-caret-right"></em>',
                    sPrevious: '<em class="fa fa-caret-left"></em>'
                }
            },
            keys: true
        });

    });





});