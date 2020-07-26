$(document).on('click', '.guess-timezone-button', function () {
   var ianiaTz = Intl.DateTimeFormat().resolvedOptions().timeZone;
   var selectlist = $($(this).attr('data-selectlist'));
   ajaxPost($(this).attr('data-href'), { timeZone: ianiaTz }, function (timezone) {
      selectlist.val(timezone)
   });
});