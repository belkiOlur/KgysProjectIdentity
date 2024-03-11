// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
$(document).ready(function() {
  $(".search").keyup(function () {
    var searchTerm = $(".search").val();
    var listItem = $('.results tbody').children('tr');
    var searchSplit = searchTerm.replace(/ /g, "'):containsi('")
    
  $.extend($.expr[':'], {'containsi': function(elem, i, match, array){
        return (elem.textContent || elem.innerText || '').toLowerCase().indexOf((match[3] || "").toLowerCase()) >= 0;
    }
  });
    
  $(".results tbody tr").not(":containsi('" + searchSplit + "')").each(function(e){
    $(this).attr('visible','false');
  });

  $(".results tbody tr:containsi('" + searchSplit + "')").each(function(e){
    $(this).attr('visible','true');
  });

  var jobCount = $('.results tbody tr[visible="true"]').length;
    $('.counter').text(jobCount + ' item');

  if(jobCount == '0') {$('.no-result').show();}
    else {$('.no-result').hide();}
		  });
});



$(function () {
    //select2 eklentimizin tanımı
    $('#select2').select2();
    //hepsiniSec kodlarımız
    $("#hepsiniSec").on("click", function () {
        if ($(this).is(':checked')) {
            $("#select2 > option").prop("selected", "selected");
            $("#select2").trigger("change");
        }
        else {
            $("#select2 > option").removeAttr("selected");
            $("#select2").trigger("change");
        }
    });
});



/*model resim başlangıç*/


//// Open the Modal
//function openModal() {
//    document.getElementById("myModal").style.display = "block";
//}

//// Close the Modal
//function closeModal() {
//    document.getElementById("myModal").style.display = "none";
//}

//var slideIndex = 1;
//showSlides(slideIndex);

//// Next/previous controls
//function plusSlides(n) {
//    showSlides(slideIndex += n);
//}

//// Thumbnail image controls
//function currentSlide(n) {
//    showSlides(slideIndex = n);
//}

//function showSlides(n) {
//    var i;
//    var slides = document.getElementsByClassName("mySlides");
//    var dots = document.getElementsByClassName("demo");
//    var captionText = document.getElementById("caption");
//    if (n > slides.length) { slideIndex = 1 }
//    if (n < 1) { slideIndex = slides.length }
//    for (i = 0; i < slides.length; i++) {
//        slides[i].style.display = "none";
//    }
//    for (i = 0; i < dots.length; i++) {
//        dots[i].className = dots[i].className.replace(" active", "");
//    }
//    slides[slideIndex - 1].style.display = "block";
//    dots[slideIndex - 1].className += " active";
//    captionText.innerHTML = dots[slideIndex - 1].alt;
//}

/*model resim bitiş*/



const staticBackdrop = document.getElementById('staticBackdrop')
if (staticBackdrop) {
    staticBackdrop.addEventListener('show.bs.modal', event => {
        // Button that triggered the modal
        const button = event.relatedTarget
        // Extract info from data-bs-* attributes
        const recipient = button.getAttribute('data-bs-id')
        const recipients = button.getAttribute('data-bs-name')
        // If necessary, you could initiate an Ajax request here
        // and then do the updating in a callback.

        // Update the modal's content.
        const modalTitle = staticBackdrop.querySelector('.modal-title')
        const modalCourseId = staticBackdrop.querySelector('#recipient-name')
        const modalCourseName = staticBackdrop.querySelector('#recipients-name')

        
        modalCourseId.value = recipient
        modalCourseName.value = recipients
    })
}

const staticBackdropp = document.getElementById('staticBackdropp')
if (staticBackdropp) {
    staticBackdropp.addEventListener('show.bs.modal', event => {
        // Button that triggered the modal
        const button = event.relatedTarget
        // Extract info from data-bs-* attributes
        const recipient = button.getAttribute('data-bs-id')
        const recipients = button.getAttribute('data-bs-name')
        // If necessary, you could initiate an Ajax request here
        // and then do the updating in a callback.

        // Update the modal's content.
        const modalTitle = staticBackdropp.querySelector('.modal-title')
        const modalCourseId = staticBackdropp.querySelector('#recipient-name')
        const modalCourseName = staticBackdropp.querySelector('#recipients-name')

       
        modalCourseId.value = recipient
        modalCourseName.value = recipients
    })
}

const staticBackdroppp = document.getElementById('staticBackdropp')
if (staticBackdroppp) {
    staticBackdroppp.addEventListener('show.bs.modal', event => {
        // Button that triggered the modal
        const button = event.relatedTarget
        // Extract info from data-bs-* attributes
        const recipient = button.getAttribute('data-bs-id')
        const recipients = button.getAttribute('data-bs-name')
        // If necessary, you could initiate an Ajax request here
        // and then do the updating in a callback.

        // Update the modal's content.
        const modalTitle = staticBackdroppp.querySelector('.modal-title')
        const modalCourseId = staticBackdroppp.querySelector('#recipient-name')
        const modalCourseName = staticBackdroppp.querySelector('#recipients-name')


        modalCourseId.value = recipient
        modalCourseName.value = recipients
    })
}

const staticBackdropppp = document.getElementById('staticBackdropp')
if (staticBackdropppp) {
    staticBackdropppp.addEventListener('show.bs.modal', event => {
        // Button that triggered the modal
        const button = event.relatedTarget
        // Extract info from data-bs-* attributes
        const recipient = button.getAttribute('data-bs-id')
        const recipients = button.getAttribute('data-bs-name')
        // If necessary, you could initiate an Ajax request here
        // and then do the updating in a callback.

        // Update the modal's content.
        const modalTitle = staticBackdropppp.querySelector('.modal-title')
        const modalCourseId = staticBackdropppp.querySelector('#recipient-name')
        const modalCourseName = staticBackdropppp.querySelector('#recipients-name')


        modalCourseId.value = recipient
        modalCourseName.value = recipients
    })
}
const staticBackdropdate = document.getElementById('staticBackdropdate')
if (staticBackdropdate) {
    staticBackdropdate.addEventListener('show.bs.modal', event => {
        // Button that triggered the modal
        const button = event.relatedTarget
        // Extract info from data-bs-* attributes
        const recipient = button.getAttribute('data-bs-id')
        const recipients = button.getAttribute('data-bs-date')
        const recipientss = button.getAttribute('data-bs-date2')
        // If necessary, you could initiate an Ajax request here
        // and then do the updating in a callback.

        // Update the modal's content.
        const modalTitle = staticBackdropdate.querySelector('.modal-title')
        const modalCourseId = staticBackdropdate.querySelector('#recipient-name')
        const modalCourseDate = staticBackdropdate.querySelector('#recipients-name')
        const modalCourseDate2 = staticBackdropdate.querySelector('#recipientss-name')

        modalCourseId.value = recipient
        modalCourseDate.value = recipients
        modalCourseDate2.value = recipientss
    })
}
const staticBackdropSil = document.getElementById('staticBackdropSil')
if (staticBackdropSil) {
    staticBackdropSil.addEventListener('show.bs.modal', event => {
        // Button that triggered the modal
        const button = event.relatedTarget
        // Extract info from data-bs-* attributes
        const recipient = button.getAttribute('data-bs-id')
        const recipients = button.getAttribute('data-bs-date')
        const recipientss = button.getAttribute('data-bs-date2')
        // If necessary, you could initiate an Ajax request here
        // and then do the updating in a callback.

        // Update the modal's content.
        const modalTitle = staticBackdropSil.querySelector('.modal-title')
        const modalCourseId = staticBackdropSil.querySelector('#recipient-name')
        const modalCourseDate = staticBackdropSil.querySelector('#recipients-name')
        const modalCourseDate2 = staticBackdropSil.querySelector('#recipientss-name')

        modalCourseId.value = recipient
        modalCourseDate.value = recipients
        modalCourseDate2.value = recipientss
    })
}
//Modal validation Başlangıç
// Example starter JavaScript for disabling form submissions if there are invalid fields
(() => {
    'use strict'

    // Fetch all the forms we want to apply custom Bootstrap validation styles to
    const forms = document.querySelectorAll('.needs-validation')

    // Loop over them and prevent submission
    Array.from(forms).forEach(form => {
        form.addEventListener('submit', event => {
            if (!form.checkValidity()) {
                event.preventDefault()
                event.stopPropagation()
            }

            form.classList.add('was-validated')
        }, false)
    })
})()


document.addEventListener("DOMContentLoaded", function () {
    // make it as accordion for smaller screens
    if (window.innerWidth < 992) {

        // close all inner dropdowns when parent is closed
        document.querySelectorAll('.navbar .dropdown').forEach(function (everydropdown) {
            everydropdown.addEventListener('hidden.bs.dropdown', function () {
                // after dropdown is hidden, then find all submenus
                this.querySelectorAll('.submenu').forEach(function (everysubmenu) {
                    // hide every submenu as well
                    everysubmenu.style.display = 'none';
                });
            })
        });

        document.querySelectorAll('.dropdown-menu a').forEach(function (element) {
            element.addEventListener('click', function (e) {
                let nextEl = this.nextElementSibling;
                if (nextEl && nextEl.classList.contains('submenu')) {
                    // prevent opening link if link needs to open dropdown
                    e.preventDefault();
                    if (nextEl.style.display == 'block') {
                        nextEl.style.display = 'none';
                    } else {
                        nextEl.style.display = 'block';
                    }

                }
            });
        })
    }
    // end if innerWidth
});
// DOMContentLoaded  end


//spinner start
$(document).ready(function () {
    $('#btnSubmit').click(function () {
        $('.spinner').css('display', 'block');
    });
});
//spinner finish