var dataTable;

$(document).ready(function () {
    var url = window.location.search;
    if (url.includes("Approved"))
        loadDataTable("Approved");
    else {
        if (url.includes("ReadyForPickup"))
            loadDataTable("ReadyForPickup");
        else {
            if (url.includes("Cancelled"))
                loadDataTable("Cancelled");
            else
                loadDataTable("All");
        }
    }

});

function loadDataTable(status) {
    dataTable = $('#tblData').DataTable({
        "ajax": { url: "/order/getall?status=" + status },
        "columns": [
            { data: 'orderHeaderId', "width": "5%" },
            { data: 'email', "width": "15%" },
            { data: 'name', "width": "15%" },
            { data: 'phone', "width": "15%" },
            { data: 'orderStatus', "width": "15%" },
            { data: 'orderTotal', "width": "15%" },
            {
                data: 'orderHeaderId',
                "render": function (data) {
                    debugger;
                    return `<div class="w-75 btn-group" role="group">
                        <a href="/Order/Detail?orderId=${data}" class="btn btn-primary mx-2">Detail</a>
                    </div>`
                },
                "width": "10%"
            }
        ]
        //,"createdRow": function (row, data, dataIndex) {
        //    debugger
        //    console.log('OrderHeaderId:', data.orderHeaderId);
        //}
    })
}
