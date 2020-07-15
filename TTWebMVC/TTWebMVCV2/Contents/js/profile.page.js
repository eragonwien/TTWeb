// opens modal to add new credential
$(document).on('click', '#facebook-credentials-list .add-button', function () {
   // resets form
   resetForm($('#facebook-credentials-list .modal form:first'));

   // opens modal
   $('#facebook-credentials-list #updatefacebookcredential-modal').trigger('active');
});

// open existing credential for editing
$(document).on('click', '#facebook-credentials-list .facebook-credential-button .is-link', function () {
   var modalForm = $('#facebook-credentials-list .modal form:first');
   if (modalForm) {
      resetForm(modalForm);

      modalForm.find('input[name="Username"]').val($(this).attr('data-username'));
      modalForm.find('input[name="Password"]').val($(this).attr('data-password'));
      modalForm.find('input[name="id"]').val($(this).attr('data-id'));
   }

   // opens modal
   $('#facebook-credentials-list #updatefacebookcredential-modal').trigger('active');
});

// save credetial
$(document).on('click', '#facebook-credentials-list .modal .save-button', function () {
   var modalForm = $('#facebook-credentials-list .modal form:first');
   ajaxPostForm(modalForm, onSaveCredentialSuccess)

   function onSaveCredentialSuccess(result) {
      $('#facebook-credentials-list .credentials-list').html(result);
      modalForm.trigger('inactive');
   }
});

// opens modal asking user to confirm the deletion of credential
$(document).on('click', '#facebook-credentials-list .facebook-credential-button .is-delete', function (e) {
   e.preventDefault();
   const deleteModal = $('#facebook-credentials-list #deletefacebookcredential-modal');
   deleteModal.find('input[type="hidden"][name="username"]').val($(this).attr('data-username'));
   deleteModal.trigger('active');
});

// user confirms the deletion of credential
$(document).on('click', '#facebook-credentials-list #deletefacebookcredential-modal .delete-button', function (e) {
   // post to server
   const modalForm = $(this).closest('form');
   ajaxPost(modalForm.attr('action'), modalForm.serialize());


   // removes deleted element
   const deleteModal = $('#facebook-credentials-list #deletefacebookcredential-modal');
   const usernameInput = deleteModal.find('input[type="hidden"][name="username"]');
   var deletedEle = $(document).find('#facebook-credentials-list .facebook-credential-button .is-link[data-username="' + usernameInput.val() + '"]').closest('.facebook-credential-button');
   deletedEle.remove();

   // clears modal input & shows modal
   usernameInput.val('');
   deleteModal.trigger('inactive');
});

// opens modal to add new friend
$(document).on('click', '#facebook-friends-list .add-button', function () {
   // resets form
   resetForm($('#facebook-friends-list .modal form:first'));

   // opens modal
   $('#facebook-friends-list #updatefacebookfriend-modal').trigger('active');
});

// open existing friend for editing
$(document).on('click', '#facebook-friends-list .facebook-friends-button .is-link', function () {
   var modalForm = $('#facebook-friends-list .modal form:first');
   if (modalForm) {
      resetForm(modalForm);

      modalForm.find('input[name="Name"]').val($(this).attr('data-name'));
      modalForm.find('input[name="ProfileLink"]').val($(this).attr('data-profileLink'));
      modalForm.find('input[name="id"]').val($(this).attr('data-id'));
   }

   // opens modal
   $('#facebook-friends-list #updatefacebookfriend-modal').trigger('active');
});

// save friend
$(document).on('click', '#facebook-friends-list .modal .save-button', function () {
   var modalForm = $('#facebook-friends-list .modal form:first');
   ajaxPostForm(modalForm, onSaveFriendSuccess)

   function onSaveFriendSuccess(result) {
      $('#facebook-friends-list .friends-list').html(result);
      modalForm.trigger('inactive');
   }
});

// opens modal asking user to confirm the deletion of friend
$(document).on('click', '#facebook-friends-list .facebook-friends-button .is-delete', function (e) {
   e.preventDefault();
   const deleteModal = $('#facebook-friends-list #deletefacebookfriend-modal');
   deleteModal.find('input[type="hidden"][name="id"]').val($(this).attr('data-id'));
   deleteModal.trigger('active');
});

// user confirms the deletion of friend
$(document).on('click', '#facebook-friends-list #deletefacebookfriend-modal .delete-button', function (e) {
   // post to server
   const modalForm = $(this).closest('form');
   ajaxPost(modalForm.attr('action'), modalForm.serialize());


   // removes deleted element
   const deleteModal = $('#facebook-friends-list #deletefacebookfriend-modal');
   const idInput = deleteModal.find('input[type="hidden"][name="id"]');
   var deletedEle = $(document).find('#facebook-friends-list .facebook-friends-button .is-link[data-id="' + idInput.val() + '"]').closest('.facebook-friends-button');
   deletedEle.remove();

   // clears modal input & shows modal
   idInput.val('');
   deleteModal.trigger('inactive');
});
