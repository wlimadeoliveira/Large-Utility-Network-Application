// DATATABLES

(function() {
    'use strict';

    $(initDatatables);

    function initDatatables() {

        if (!$.fn.DataTable) return;

        $('#datatable1').DataTable({
            'paging': true,
            'ordering': true,
            'info': true,
            responsive: true,
            oLanguage: {
                sSearch: '<em class="fas fa-search"></em>',
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
            dom: 'Bfrtip',
            buttons: [
                { extend: 'copy', className: 'btn-info' },
                { extend: 'csv', className: 'btn-info' },
                { extend: 'excel', className: 'btn-info', title: 'XLS-File' },
                { extend: 'pdf', className: 'btn-info', title: $('title').text() },
                { extend: 'print', className: 'btn-info' }
            ]
        });

        var table = $('#datatable2').DataTable({
            'paging': true, // Table pagination
            'ordering': true, // Column ordering
            'info': true, // Bottom left status text
            'pageLength': 50,
            //"lengthMenu": [[10, 25, 50, -1], [10, 25, 50, "All"]],
            orderCellsTop: true,
            responsive: true,
            // Text translation options
            // Note the required keywords between underscores (e.g _MENU_)
            oLanguage: {
                sSearch: 'Search all columns:',                
                aLengthMenu: [[10, 20, 25, 50, -1], [10, 20, 25, 50, 'All']],        
                info: 'Showing page _PAGE_ of _PAGES_',
                zeroRecords: 'Nothing found - sorry',
                infoEmpty: 'No records available',
                infoFiltered: '(filtered from _MAX_ total records)',
                oPaginate: {
                    sNext: '<em class="fa fa-caret-right"></em>',
                    sPrevious: '<em class="fa fa-caret-left"></em>'
                },
                keys: true
            },
            // Datatable Buttons setup
            dom: 'Blfrtip',
            buttons: [
                {
                    extend: 'copy', className: 'btn-info', exportOptions: {
                        columns: ':visible'
                    } },
                {
                    extend: 'csv', className: 'btn-info', exportOptions: {
                        columns: ':visible'
                    } },
                { extend: 'excel', className: 'btn-info', title: 'XLS-File' },
                {
                    extend: 'pdf', className: 'btn-info', title: $('title').text(), exportOptions: {
                        columns: ':visible'
                    } },
                { extend: 'print', className: 'btn-info' }
            ]
        });

        var table1 = $('#datatable4').DataTable({
            'paging': true, // Table pagination
            "aaSorting": [],
            'ordering': true, // Column ordering
            'info': true, // Bottom left status text
            "bLengthChange": false,
            'pageLength': 300,
            //"lengthMenu": [[10, 25, 50, -1], [10, 25, 50, "All"]],
            orderCellsTop: true,
            responsive: true,
            // Text translation options
            // Note the required keywords between underscores (e.g _MENU_)
            oLanguage: {
                sSearch: 'Search all columns:',
             
                info: 'Showing page _PAGE_ of _PAGES_',
                zeroRecords: 'Nothing found - sorry',
                infoEmpty: 'No records available',
                infoFiltered: '(filtered from _MAX_ total records)',
                oPaginate: {
                    sNext: '<em class="fa fa-caret-right"></em>',
                    sPrevious: '<em class="fa fa-caret-left"></em>'
                },
                keys: true
            },
            // Datatable Buttons setup
            dom: 'Blfrtip',
            buttons: [
                {
                    extend: 'copy', className: 'btn-info', exportOptions: {
                        columns: ':visible'
                    }
                },
                {
                    extend: 'csv', className: 'btn-info', exportOptions: {
                        columns: ':visible'
                    }
                },
                { extend: 'excel', className: 'btn-info', title: 'XLS-File' },
                {
                    extend: 'pdf', className: 'btn-info', title: $('title').text(), exportOptions: {
                        columns: ':visible'
                    }
                },
                { extend: 'print', className: 'btn-info' }
            ]
        });





        $("#printbuttons").append($("div.dt-buttons").addClass('float-right'));
        $("#datatable2_length").addClass('float-right');

        //function for togglebuttons
   
        var items = "";
        var index = 0
        $("thead .tableheader th").each(function () {
            items += '<button class="toggle-vis btn btn-secondary" type="button" data-column="' + index + '">' + $(this).text() + '</button>';
            index++;
        });
        $('#togglecolumn').html(items);
       

        $('button.toggle-vis').on('click', function (e) {
            e.preventDefault();
            // Get the column API object
            var column = table.column($(this).attr('data-column'));
            // Toggle the visibility
            column.visible(!column.visible());
        });

      
        // Setup - add a text input to each header cell
        $('#datatable2 thead .filters th').each(function () {
            var title = $('#datatable2 thead tr:eq(0) th').eq($(this).index()).text();
            $(this).html('<input type="text" placeholder="Search ' + title + '" />');
        });

        if ($('#typedropdown').length > 0) {
            $('#typedropdown').html('<select class="chosen-select"> <option value="" selected>All Types</option>' + _typeoptions + '</select>')
            $("select").chosen();
            $("chosen-select").chosen();
        }
            
       

        // Apply the search
        table.columns().eq(0).each(function (colIdx) {
            $('input', $('.filters th')[colIdx]).on('keyup change', function () {
                table
                    .column(colIdx)
                    .search(this.value)
                    .draw();
            });
        });

        table.columns().eq(0).each(function (colIdx) {
            $('select', $('.filters th')[colIdx]).on('keyup change', function () {
                table
                    .column(colIdx)
                    .search(this.value)
                    .draw();
            });
        });
  
      



  








        

        //Draws row after History Quick add on Product Details
        /*$('#buttonhistory').click(function (evt) {
            evt.preventDefault();
            var comment = $('#comment').val();
            var datetime = new Date().toLocaleString()
            var user = $('#useremail').val();
            var status = $('#historytypes option:selected').text();
            var badge = $('#historytypes option:selected').attr('class');
            var rowNode = table1.row.add([status, comment, datetime, user]).order([2, 'desc']).draw().node();

            $(rowNode).children(':first').addClass(badge);         
        });*/
      




    }


     


})();