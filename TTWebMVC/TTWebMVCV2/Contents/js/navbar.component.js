$('.navbar-burger').click(function (e) {
   e.preventDefault();
   $(this).toggleClass('is-active');
   $($(this).data('target')).toggleClass('is-active');
});