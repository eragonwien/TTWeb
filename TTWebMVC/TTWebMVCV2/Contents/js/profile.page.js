$('#facebook-credentials-list .add-button').on('click', function () {
   $('#facebook-credentials-list #updatefacebookcredential-modal').trigger('active');
   var modalForm = $('#facebook-credentials-list .modal form:first');
   if (modalForm) {
      modalForm.find(':input').val('');
      resetPasswordToggle($(modalForm).find('.password-toggle'));
   }
});

$('#facebook-credentials-list .facebook-credential-button .is-link').on('click', function () {
   $('#facebook-credentials-list #updatefacebookcredential-modal').trigger('active');
   var modalForm = $('#facebook-credentials-list .modal form:first');
   if (modalForm) {
      modalForm.find(':input').val('');
      resetPasswordToggle($(modalForm).find('.password-toggle'));
      
      modalForm.find('input[name="Username"]').val($(this).attr('data-username'));
      modalForm.find('input[name="Password"]').val($(this).attr('data-password'));
      modalForm.find('input[name="id"]').val($(this).attr('data-id'));
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
         .addClass('facebook-credential-button');

      newButton.find('.is-link')
         .attr('data-username', savedUsername)
         .attr('data-password', savedPassword)
         .text(savedUsername);
      newButton.find('.is-delete').attr('data-password', savedPassword);

      let existingButton = $('#facebook-credentials-list .credential-list .facebook-credential-button a[data-username="' + savedUsername + '"]');
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

$('#facebook-credentials-list .facebook-credential-button .is-delete').click(function (e) {
   e.preventDefault();
   const deleteModal = $('#facebook-credentials-list #deletefacebookcredential-modal');
   deleteModal.find('input[type="hidden"][name="username"]').val($(this).attr('data-username'));
   deleteModal.trigger('active');
});

$('#facebook-credentials-list #deletefacebookcredential-modal .delete-button').click(function (e) {
   // post to server
   const modalForm = $(this.closest('form'));
   ajaxPost(modalForm.attr('action'), modalForm.serialize());

   // removes deleted element
   const deleteModal = $('#facebook-credentials-list #deletefacebookcredential-modal');
   const usernameInput = deleteModal.find('input[type="hidden"][name="username"]');
   var deletedEle = $('#facebook-credentials-list .facebook-credential-button .is-link[data-username="' + usernameInput.val() + '"]').closest('.facebook-credential-button');
   deletedEle.remove();

   // clears modal input & shows modal
   usernameInput.val('');
   deleteModal.trigger('inactive');
});