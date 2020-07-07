function ajaxPost(url, data, onSuccess) {
   $.ajax({
      type: 'POST',
      url: url,
      beforeSend: onBeforeSend,
      data: data,
      success: onSuccess
   });

   function onBeforeSend(req) {
      var token = $('#_AjaxFormAntiForgerySrc input[name="__RequestVerificationToken"]').val();
      req.setRequestHeader('RequestVerificationToken', token);
   }
}

function navigateToUrl(url) {
   location.href = url;
}

function parseNumber(value, base) {
   const parsedValue = parseInt(value, base);
   if (isNaN(parsedValue)) {
      return -1;
   }
   return parsedValue;
}

$('.modal button.modal-close').click(function () {
   $(this).closest('.modal').trigger('inactive');
});

$('.modal').on('active', function () {
   $(this).addClass('is-active');
});

$('.modal').on('inactive', function () {
   $(this).removeClass('is-active');
});

$('.password-toggle').click(function () {
   const input = $(this).closest('.field').find('input:first');
   const copyInput = input.clone();
   const inputType = copyInput.prop('type');
   copyInput.attr('type', inputType === 'password' ? 'text' : 'password');
   copyInput.insertAfter(input);
   input.remove();

   if (copyInput.prop('type') === 'password') {
      $(this).find('.icon :first-child').prop('name', 'eye-off');
   }
   else {
      $(this).find('.icon :first-child').prop('name', 'eye');
   }
});

function resetPasswordToggle(toggleButton) {
   const input = $(toggleButton).closest('.field').find('input:first');
   const copyInput = input.clone();
   copyInput.attr('type', 'password');
   copyInput.insertAfter(input);
   input.remove();
   $(toggleButton).find('.icon :first-child').prop('name', 'eye-off');
}


