// Call the dataTables jQuery plugin
$(document).ready(function() {
    $('#dataTable').DataTable({
        "pageLength": 3,
        "lengthMenu": [[3, 6, -1],[3, 6 ,"All"]]
    });
});
