// $('#btn').on({
//     click:function(){
//         $(this).css('background-color','green') 
//     },
//     dblclick:function(){
//         $(this).css('background-color','yellow') 
//     },
//     mouseenter:function(){
//         $(this).css('background-color','orange') 
//     },
//     mouseleave:function(){
//         $(this).css('background-color','red') 
//     }
// })
$('.LL').on({
  click:function(){
    $('.Login-Logout').css('display','block')
  },
})
$('.Login-Logout').on({
  mouseleave:function(){
    $('.Login-Logout').css('display','none')
    $('.Login-Logout').css('transition','1s')
  }
})

AOS.init();

$('.header-plugin').slick({
    infinite: false,
    slidesToShow: 1,
    slidesToScroll: 1,
    speed:1000,
    autoplay: true,
  autoplaySpeed: 2500,
  prevArrow:'<span class="next"><i class="fa-solid fa-chevron-right"></i></span>',
    nextArrow:'<span class="prev"><i class="fa-solid fa-chevron-left"></i></span>', 
  });
           
   $('#slider').slick({
    dots: false,
    infinite: true,
    speed: 800,
    fade: true,
    autoplay: true,
    autoplaySpeed: 3000,
    prevArrow:'<span class="next"><i class="fa-solid fa-chevron-right"></i></span>',
    nextArrow:'<span class="prev"><i class="fa-solid fa-chevron-left"></i></span>',
    cssEase: 'linear',
    responsive: [
        {
          breakpoint: 1024,
          settings: {
            dots: false,
    infinite: true,
          }
        },
        {
          breakpoint: 600,
          settings: {
            dots: false,
            infinite: true,
          }
        },
        {
          breakpoint: 480,
          settings: {
            dots: false,
            infinite: true,
          }
        }
      ]
  });
  $('.shop-slider').slick({
    dots:true,
    infinite: true,
    autoplay: true,
    autoplaySpeed: 5000,
    slidesToShow: 1,
    slidesToScroll: 1
  });
  $('.Arrival-shop').slick({
    infinite: false,
    slidesToShow: 3,
    slidesToScroll: 1,
    speed:300,
    prevArrow:'<span class="next"><i class="fa-solid fa-chevron-right"></i></span>',
    nextArrow:'<span class="prev"><i class="fa-solid fa-chevron-left"></i></span>', 
  });
  $('.Product-tab-menu button').click(function() {
    var Id = $(this).attr('id');
    $('.tab-menu').each(function() {
      if ($(this).attr('id') === Id) {
          $(this).removeClass('d-none');
      } else {
          $(this).addClass('d-none');
      }
    });
  }); 
  $('.Product-tab-menu button').click(function() {
    let btn = $('.Active');
    btn.removeClass('Active');
    $(this).addClass('Active');
    
  });
  $(document).ready(function() {
    $('.angles, .angle').click(function() {
      $('.form').slideToggle();
      $('.angles, .angle').toggle();
    });
    $('.angle2, .angles2').click(function() {
      $('.form-product-type').slideToggle();
      $('.angle2, .angles2').toggle();
    });
    $('.angle3, .angles3').click(function() {
      $('.form-brand').slideToggle();
      $('.angle3, .angles3').toggle();
    });
    $('.angle4, .angles4').click(function() {
      $('.form-size').slideToggle();
      $('.angle4, .angles4').toggle();
    });
    $('.angle5, .angles5').click(function() {
      $('.form-color').slideToggle();
      $('.angle5, .angles5').toggle();
    });
    $('.angle6, .angles6').click(function() {
      $('.form-price').slideToggle();
      $('.angle6, .angles6').toggle();
    }); 
    $('.Size-clc li').click(function() {
      let btn = $('.Active');
      btn.removeClass('Active');
      $(this).addClass('Active');
    });
});


var inputStyle = {
  "top": "-10px",
  "left": "620px",
  "font-size": "13px",
  "transition": ".3s"
};

var $inputs = $('input[id^="form-"]');
var $labels = $inputs.next('label');

$inputs.on('click', function() {
  var $input = $(this);
  var $label = $input.next('label')
  $label.css(inputStyle);
});
$inputs.on('input', function() {
  var $input = $(this);
  var $label = $input.next('label');

  if ($input.val() !== '') {
    $label.css(inputStyle);
  } else {
    $label.css({
      "top": "17px",
      "left": "640px",
      "font-size": "17px",
      "transition": ".3s"
    }); 
  }
});


$('body').on('click', function(event) {
  if (!$(event.target).is('input[id^="form-"]')) {
    $('input[id^="form-"]').each(function() {
      if ($(this).val() === '') {
        var $label = $(this).next('label');
        $label.css({
          "top": "17px",
          "left": "640px",
          "font-size": "17px",
          "transition": ".3s"
        });
      }
    });
  }
});

window.onscroll = function(){
  if( document.body.scrollTop>150 || document.documentElement.scrollTop>150){
  }
}

$('.slider-for').slick({
  slidesToShow: 1,
  slidesToScroll: 1,
  arrows: false,
  fade: true,
  asNavFor: '.slider-nav',
  prevArrow:'<span class="next1"><i class="fa-solid fa-chevron-right"></i></span>',
  nextArrow:'<span class="prev1"><i class="fa-solid fa-chevron-left"></i></span>', 

  
});
$('.slider-nav').slick({
  slidesToShow: 3,
  slidesToScroll: 1,
  asNavFor: '.slider-for',
  dots: false,
  centerMode: true,
  focusOnSelect: true,
  prevArrow:'<span class="next"><i class="fa-solid fa-chevron-right"></i></span>',
  nextArrow:'<span class="prev"><i class="fa-solid fa-chevron-left"></i></span>', 
});
	


const rangeInput = document.querySelectorAll('.range-input input');
priceInput = document.querySelectorAll('.price-input input');
progresss = document.querySelector('.sliders .progresss');

let priceGap = 49;

priceInput.forEach(input=>{
  input.addEventListener("input",e=>{
    let minVal = parseInt(priceInput[0].value),
     maxVal = parseInt(priceInput[1].value);

     if((maxVal-minVal >= priceGap) && maxVal <= 1000){
      if(e.target.className === "input-min"){
        rangeInput[0].value=minVal
        progresss.style.left = (minVal/rangeInput[0].max)*100 + "%";
      }else{
        rangeInput[1].value = maxVal
        progresss.style.right =100 - (maxVal/rangeInput[1].max)*100 + "%"; 
      }
     }
  })
})

rangeInput.forEach(input=>{
  input.addEventListener("input",e=>{
    let minVal = parseInt(rangeInput[0].value),
     maxVal = parseInt(rangeInput[1].value);

     if(maxVal-minVal<priceGap){
      if(e.target.className === "range-min"){
        rangeInput[0].value=maxVal-priceGap
      }else{
        rangeInput[1].value = minVal + priceGap
      }
     }else{
      priceInput[0].value = minVal;
      priceInput[1].value = maxVal;
      progresss.style.left = (minVal/rangeInput[0].max)*100 + "%";
      progresss.style.right =100 - (maxVal/rangeInput[1].max)*100 + "%"; 
     }
  })
})





$('.Size li').click(function(){
 let Size = $('.Active')
 Size.removeClass('Active');
  $(this).addClass('Active');
})


$('.Color li').click(function(){
  let Color = $('.Active')
  Color.removeClass('Active');
   $(this).addClass('Active');
 })

 $('.tab-account button').click(function(){
  let button = $('.Active')
  button.removeClass('Active');
   $(this).addClass('Active');
 })
 $('.tab-account button').click(function() {
  var Id = $(this).attr('id');
  $('.tab-m').each(function() {
    if ($(this).attr('id') === Id) {
        $(this).removeClass('d-none');
    } else {
        $(this).addClass('d-none');
    }
  });
}); 