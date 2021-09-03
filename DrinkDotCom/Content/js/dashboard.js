$("#menu-toggle").click(function (e) {
    e.preventDefault();
    $("#wrapper").toggleClass("toggled");
});

$("#btnLogOff").click(function () {
    $("#logOffForm").submit();
});

function activatePageLink() {
    $(".list-group-item", "#sidebar-wrapper").removeClass("active");

    var activePage = $("#activePage").val();

    $("a.list-group-item[data-page="+activePage+"]").addClass("active");
}

function updateEditFields() {
    for (instance in CKEDITOR.instances) {
        CKEDITOR.instances[instance].updateElement();
    }
}
function removeThis(elem) {
    $(elem).remove();
}

function removeImage(elem) {
    $(elem).parents(".card").remove();
}

$(document).ready(function () {
    //$("form").submit(function () {
    //    if ($("form").valid())
    //    {
    //        $(this).find(":input").filter(function () { return !this.value; }).attr("disabled", "disabled");
    //        return true; // ensure form still submits
    //    }
    //});
});