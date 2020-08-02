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

function reloadPage() {
   location.reload();
}

function parseNumber(value, base) {
   const parsedValue = parseInt(value, base);
   if (isNaN(parsedValue)) {
      return -1;
   }
   return parsedValue;
}

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

$('.toggle-ajax').change(function () {
   let data = getDataFromDataAttributes(this);
   data.active = $(this).is(':checked');
   ajaxPost($(this).attr('data-href'), data);
});

$(document).on('click', 'a[href][href!=null].ajax', function (e) {
   e.preventDefault();
   let data = getDataFromDataAttributes(this);
   let reloadOnSuccess = $(this).attr('data-success-reload');
   ajaxPost($(this).attr('href'), data, function () {
      if (reloadOnSuccess) {
         reloadPage();
      }
   });
});

$('.modal-ajax-trigger[data-modal-target][data-modal-target!=""][data-modal-href][data-modal-href!=""]').click(triggerAjaxModal);

function triggerAjaxModal() {
   const modalHref = $(this).attr('data-modal-href');
   const modalTarget = $($(this).attr('data-modal-target'));

   let modalData = getDataFromDataAttributes(this);

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

function getDataFromDataAttributes(ele) {
   let data = {};

   const id = $(ele).attr('data-params-id');
   if (id) {
      data.id = id;
   }

   return data;
}

$(document).on('change', 'input[type="checkbox"]', function () {
    var checkboxParent = $(this).parent();
    if (checkboxParent.hasClass('button checkbox-button')) {
        checkboxParent.toggleClass('is-success', $(this).is(':checked'));
    }
});

$(document).on('change', 'select', function () {
    $(this).valid();
});