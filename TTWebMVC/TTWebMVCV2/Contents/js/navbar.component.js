$('.navbar-burger').click(function (e) {
   e.preventDefault();
   $(this).toggleClass('is-active');
   $($(this).data('target')).toggleClass('is-active');
});

$('#logout-button').click(function (e) {
   e.preventDefault();
   ajaxPost($(this).attr('href'), null, navigateToUrl);
});