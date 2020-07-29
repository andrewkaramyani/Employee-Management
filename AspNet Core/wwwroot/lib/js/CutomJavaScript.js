function confirmDelete(uniquid,isdeletclick)
{
    var deleteSpan = "deleteSpan_" + uniquid
    var confirmspan = "confirmDeleteSpan_" + uniquid

    if (isdeletclick)
    {
        $("#"+ deleteSpan).hide();
        $("#"+ confirmspan).show();
    } else {
        $("#"+ deleteSpan).show();
        $("#"+ confirmspan).hide();
    }
}