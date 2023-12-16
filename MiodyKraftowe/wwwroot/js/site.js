$(document).ready(function () {
    $('.sidenav').sidenav();
});
document.addEventListener('DOMContentLoaded', function () {
    var elems = document.querySelectorAll('.fixed-action-btn');
    var instances = M.FloatingActionButton.init(elems, {
        hoverEnabled: false
    });
});
$(".dropdown-trigger").dropdown();
