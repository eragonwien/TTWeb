$(document).ready(function () {
   //attachDateTimePicker();
});

//function attachDateTimePicker() {
//   bulmaCalendar.attach('input[type="time"]');
//   bulmaCalendar.attach('input[type="date"]');
//   $(document).find('button.datetimepicker-clear-button').attr('type', 'button');
//}

// closes modal window
$(document).on('click', '.modal button.modal-close, .modal [data-modal-close="true"]', function () {
   $(this).closest('.modal').trigger('inactive');
});

// open modal listener
$('.modal').on('active', function () {
   $(this).addClass('is-active');
});

// close modal listener
$('.modal').on('inactive', function () {
   $(this).removeClass('is-active');
});

// triggers open modal
$('.modal-trigger[data-modal-target][data-modal-target!=""]').click(function () {
   const modalTarget = $($(this).attr('data-modal-target'));
   modalTarget.trigger('active');
});

// closes closest notification
$('.notification .delete').click(function () {
   $(this).closest('.notification').remove();
});

