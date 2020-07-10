var typingTimer;

$('form.autoSubmit').on('input', function () {
   clearTimeout(typingTimer);
   const form = $(this);
   const delaySecond = Math.round(parseFloat(form.attr('data-auto-submit-delay-second')));
   if (delaySecond > 0) {
      typingTimer = setTimeout(onDoneTyping, delaySecond * 1000);
   }

   function onDoneTyping() {
      form.trigger('doneTyping');
   }
});

$('form.autoSubmit').on('doneTyping', function () {
   $.post($(this).prop('action'), $(this).serialize());
});


