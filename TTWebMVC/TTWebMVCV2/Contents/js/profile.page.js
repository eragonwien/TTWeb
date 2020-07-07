$('#facebook-credentials-list .add-button').on('click', function () {
   $('#facebook-credentials-list .modal').trigger('active');
   var modalForm = $('#facebook-credentials-list .modal form:first');
   if (modalForm) {
      modalForm.find(':input').val('');
      resetPasswordToggle($(modalForm).find('.password-toggle'));
   }
});

$('#facebook-credentials-list .facebook-credential-button').on('click', function () {
   $('#facebook-credentials-list .modal').trigger('active');
   var modalForm = $('#facebook-credentials-list .modal form:first');
   if (modalForm) {
      modalForm.find(':input').val('');
      resetPasswordToggle($(modalForm).find('.password-toggle'));
      modalForm.find('input[name="Username"]').val($(this).data('username'));
      modalForm.find('input[name="Password"]').val($(this).data('password'));
   }
});

$('#facebook-credentials-list .modal .save-button').click(function () {
   const modalTarget = $('#facebook-credentials-list .modal');
   const savedUsername = modalTarget.find('input[name="Username"]').val();
   const savedPassword = modalTarget.find('input[name="Password"]').val();

   if (savedUsername) {
      const templateButton = $('#facebook-credentials-list .credential-list .template-button.is-hidden')
      const newButton = templateButton.clone()
         .removeClass('template-button is-hidden')
         .addClass('facebook-credential-button')
         .text(savedUsername)
         .attr('data-username', savedUsername)
         .attr('data-password', savedPassword);

      let existingButton = $('#facebook-credentials-list .credential-list .facebook-credential-button[data-username="' + savedUsername + '"]');
      if (existingButton.length === 0) {
         newButton.insertAfter(templateButton);
      } else {
         existingButton.attr('data-password', savedPassword);
      }

      const modalForm = $(this.closest('form'));
      ajaxPost(modalForm.attr('action'), modalForm.serialize());
   }
   modalTarget.trigger('inactive');
});