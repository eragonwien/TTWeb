$('#logout-button').click(function (e) {
   e.preventDefault();
   $.post($(this).attr('href'), null, navigateToUrl);
});

function ajaxPost(method, data, onSuccess) {
   method = method ? method : 'POST';
   $.ajax({
      type: method,
      data: data,
      success: onSuccess
   });
}

function navigateToUrl(url) {
   location.href = url;
}

