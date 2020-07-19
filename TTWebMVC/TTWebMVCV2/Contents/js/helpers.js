function ajaxPost(url, data, onSuccess) {
   $.ajax({
      type: 'POST',
      url: url,
      beforeSend: onBeforeSend,
      data: data,
      success: onSuccess
   });
}

function onBeforeSend(req) {
   var token = $('#_AjaxFormAntiForgerySrc input[name="__RequestVerificationToken"]').val();
   req.setRequestHeader('RequestVerificationToken', token);
}

function ajaxPostForm(form, onSuccess) {
   ajaxPost($(form).attr('action'), $(form).serialize(), onSuccess);
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

$(document).on('click', '.modal button.modal-close, .modal [data-modal-close="true"]', function () {
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
      $(this).find('.icon :first-child').removeClass('fa-eye').addClass('fa-eye-slash')
   }
   else {
      $(this).find('.icon :first-child').removeClass('fa-eye-slash').addClass('fa-eye')
   }
});

function resetForm(form) {
   clearAllInputs(form);
   resetPasswordToggle($(form).find('.password-toggle'));
}

function clearAllInputs(ele) {
   $(ele).find(':input').val('');
}

function resetPasswordToggle(toggleButton) {
   const input = $(toggleButton).closest('.field').find('input:first');
   const copyInput = input.clone();
   copyInput.attr('type', 'password');
   copyInput.insertAfter(input);
   input.remove();
   $(toggleButton).find('.icon :first-child').removeClass('fa-eye').addClass('fa-eye-slash');
}

$('.modal-trigger[data-modal-target][data-modal-target!=""]').click(function () {
   const modalTarget = $($(this).attr('data-modal-target'));
   modalTarget.trigger('active');
});

$('.notification .delete').click(function () {
   $(this).closest('.notification').remove();
});

$('input[type="checkbox"]').change(function () {
   $(this).val($(this).is(':checked') ? true : false);
});

$('.toggle-ajax').change(function () {
   ajaxPostForm($(this).closest('form'));
});

$('.modal-ajax-trigger[data-modal-target][data-modal-target!=""][data-modal-href][data-modal-href!=""]').click(triggerAjaxModal);

function triggerAjaxModal() {
   const modalHref = $(this).attr('data-modal-href');
   const modalTarget = $($(this).attr('data-modal-target'));
   const modalDataId = $(this).attr('data-modal-data-id');

   let modalData = { id: modalDataId};

   $.ajax({
      type: 'GET',
      url: modalHref,
      beforeSend: onBeforeSend,
      data: modalData,
      success: onSuccess
   });

   function onSuccess(html) {
      modalTarget.html(html);
      modalTarget.trigger('active');
   }
}
