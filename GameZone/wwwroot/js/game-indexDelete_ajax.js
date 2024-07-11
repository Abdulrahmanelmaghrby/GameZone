$(document).ready(function () {
    $('.js-delete').on('click', function () {
        var btn = $(this);

    /*    console.log(btn.data('id'));*/

        const swal = Swal.mixin({
            customClass: {
                confirmButton: "btn btn-danger mx-2",
                cancelButton: "btn btn-light"
            },
            buttonsStyling: false
        });
        swal.fire({
            title: "Are you sure you want to delete the Game?",
            text: "You won't be able to revert this!",
            icon: "warning",
            showCancelButton: true,
            confirmButtonText: "Yes, delete it!",
            cancelButtonText: "No, cancel!",
            reverseButtons: true
        }).then((result) => {
            console.log(result.isConfirmed)
            if (result.isConfirmed) {

                $.ajax({
                    url: `Games/Delete/${btn.data('id')}`,
                    method: 'DELETE',
                    success: function () {
                        swal.fire({
                            title: "Deleted!",
                            text: " Game has been deleted.",
                            icon: "success"
                        });
                        btn.parents('tr').fadeOut();
                    },
                    error: function () {
                        swal.fire({
                            title: "Ooooops...!",
                            text: "Something went wrong .",
                            icon: "success"
                        });
                    }
                });
                swal.fire({
                    title: "Deleted!",
                    text: "Your file has been deleted.",
                    icon: "error"
                });
            } 
            
        });

     });
   });
        //$.ajax({
        //    url: `Games/Delete/${btn.data('id')}`,
        //    method: 'DELETE',
        //    success: function () {
        //        alert('success');
        //    },
        //    error: function () {
        //        alert('error');
        //    }
        //});
   
   
