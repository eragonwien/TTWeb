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

function parseNumber(value, base) {
   const parsedValue = parseInt(value, base);
   if (isNaN(parsedValue)) {
      return -1;
   }
   return parsedValue;
}
