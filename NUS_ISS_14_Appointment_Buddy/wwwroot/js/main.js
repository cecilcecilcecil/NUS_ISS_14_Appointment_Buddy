(function($){

    //Disable autocomplete throughout the site
    $(document).ready(function () {
        $("input:text,form").attr("autocomplete", "off");
    });

$(document).ready(function () {
    $('#increaseFont').click(function () {
        var maincurSize = parseInt($('#content').css('font-size')) + 2;
        var heroboxWidth = parseInt($('._box').css('width')) + 10;
        if (maincurSize <= 22) {
            $('#content').css('font-size', maincurSize);
            $('._box').css('width', heroboxWidth);
        }        

        //var zoom = parseFloat($('#content').css("zoom")) + 0.1;
        //if (zoom < 1.3) {
        //    $('#content').css({ "zoom": zoom });
        //}
    });

    $('#defaultFont').click(function () {
        $('#content').css('font-size', 16);
        $('._box').css('width', 191);
    });

    $('#decreaseFont').click(function () {
        var maincurSize = parseInt($('#content').css('font-size')) - 2;
        var heroboxWidth = parseInt($('._box').css('width')) - 10;
        if (maincurSize >= 16) {
            $('#content').css('font-size', maincurSize);
            $('._box').css('width', heroboxWidth);
        }
        //var zoom = parseFloat($('#content').css("zoom")) - 0.1;
        //if (zoom > 0.8) {
        //    $('#content').css({ "zoom": zoom });
        //}
    });

    // Ken JS
    $( "#tender-tabs" ).tabs();
    
    $('.tender-buttons').slick({
        infinite: true,
        slidesToShow: 5,
        slidesToScroll: 1,
        responsive: [
        {
        breakpoint: 9999,
        settings: {
            slidesToShow: 5,
            slidesToScroll: 1,
            dots: false,
            prevArrow: "<img class='slick-prev-web' src='../img/slider-arrow-left.png' />",
            nextArrow: "<img class='slick-next-web' src='../img/slider-arrow-right.png' />",
        }
        },
        {
        breakpoint: 991,
        settings: {
          slidesToShow: 5,
          slidesToScroll: 1,
            dots: false,
            prevArrow: "<img class='slick-prev' src='../img/slider-arrow-left.png' />",
            nextArrow: "<img class='slick-next' src='../img/slider-arrow-right.png' />",
        }
        },
        {
        breakpoint: 576,
        settings: {
          slidesToShow: 3,
          slidesToScroll: 1,
            dots: false,
            prevArrow: "<img class='slick-prev' src='../img/slider-arrow-left.png' />",
            nextArrow: "<img class='slick-next' src='../img/slider-arrow-right.png' />",
        }
        }
      ]
    });
  });
  
  $(document).ready(function () {
    // Joseph JS
    $(".hero-container .quick-boxes a ._box").hover(
      function() {
            var src = $(this).children('img').attr('src');
            var newSrc = src.split('.')[0];
            $(this).children('img').attr('src', newSrc + '-i.png');
      }, function() {
            var src = $(this).children('img').attr('src');
            var newSrc = src.split('-')[0];
            var partial = newSrc.indexOf('.png') != - 1 ? "" : ".png";
            $(this).children('img').attr('src', newSrc + partial);
      }
    );
    
    $(".mobile-master .burger-holder").click(function() {
      //$(this).toggleClass("is-active");
        var flag = document.getElementById('menu-icon').getAttribute('data-visible');
        if (flag == 'true') {
            $('.close-icon').addClass('burger-icon');
            $('.close-icon').removeClass('close-icon');
            $('#cross-icon').addClass('cross-icon');

            $("._page-wrapper").removeClass('fading noScroll');
            $(".side-container ").toggle("slide", { direction: "left" }, 500);
            $("._page-wrapper").removeClass('fading');
            $('.menu-list ul li.clicks.drops.open').removeClass('drops open');
            $('.menu-list ul li.clicks').children('ul').slideUp();
            $('.menu-list ul li.clicks').children('a').find('.fa-angle-down').removeClass('down');

            document.getElementById('menu-icon').setAttribute('data-visible', 'false');
        } else {
            $("._page-wrapper").addClass('fading noScroll');
            $('.burger-icon').addClass('close-icon');
            $('.burger-icon').removeClass('burger-icon');
            $('#cross-icon').removeClass('cross-icon');
            $(".side-container").toggle("slide");


            document.getElementById('menu-icon').setAttribute('data-visible', 'true');
        }
        
    });
    
    $(".side-container .side-admin .menu-admin ._close").click(function(){
      $("._page-wrapper").removeClass('fading noScroll');
    });
    
    $("._close").click(function(){
      $(".side-container ").toggle("slide", { direction: "left" }, 500);
      $("._page-wrapper").removeClass('fading');
      $('.menu-list ul li.clicks.drops.open').removeClass('drops open');
      $('.menu-list ul li.clicks').children('ul').slideUp();
      $('.menu-list ul li.clicks').children('a').find('.fa-angle-down').removeClass('down');
    });

    var $mobileClick = 0;
    var $mobileClick2 = 0;
    
    $('.menu-list ul .click-itq').click(function(){
      $mobileClick++;
      $(this).addClass('drops open');
      $('.menu-list ul li.click-itq').children('ul').slideToggle();
      if($mobileClick%2==0) {
        $(this).removeClass('drops open');
      }else {
        $(this).addClass('drops open');
      }
    });
    
    $('.menu-list ul .click-resources').click(function(){
      $mobileClick2++;
      $(this).addClass('drops open');
      $('.menu-list ul li.click-resources').children('ul').slideToggle();
      if($mobileClick2%2==0) {
        $(this).removeClass('drops open');
      }else {
        $(this).addClass('drops open');
      }
    });
    
    $('.menu-list ul li.clicks').on('click',function(){
      $('.menu-list ul li.clicks').children('a').find('.fa-angle-down').removeClass('down');
      $('.menu-list ul li.clicks.drops').children('a').find('.fa-angle-down').addClass('down');
    });
    
    $(function () {
      
      
      $('[data-toggle="help-tool"]').tooltip({
        trigger: "hover"
      })
    });
    
    $(function () {
      $('[data-toggle="setting-mobile"]').tooltip({
        trigger: "hover"
      })
      
      $('[data-toggle="help-mobile"]').tooltip({
        trigger: "hover"
      })
      
      $('[data-toggle="notif-mobile"]').tooltip({
        trigger: "hover"
      })
    });
    
    
    $('[data-toggle="setting"]').click(function(){
      $(this).tooltip('hide');
    });
    
    $('.notif').hover(function(){
      $(this).css("cursor","pointer");
    });
    
    var mobileNotif = 0;
    $('.mobile-tip').click(function(){
      $('.m-notif').fadeIn();
      mobileNotif++;
      console.log(mobileNotif);
      if(mobileNotif%2==0) {
        $('.m-notif').fadeOut();
      }else {
        $('.m-notif').fadeIn();
      }
    });
    
    
    
    $('._alert-container .alert-main .alert-closing').click(function(){
      $('._alert-container').fadeOut();
    });
    
    $('._category').niceSelect();
    
    $('[data-toggle="datepicker"]').datepicker({
      dateFormat: 'dd/mm/yy',
      dayNamesMin: ['Sun', 'Mon', 'Tue', 'Wed', 'Thu', 'Fri', 'Sat'],
      afterShow: function() {
        	$(".ui-datepicker-prev").empty().append('<i class="fa fa-chevron-left _fix"></i>')
          $(".ui-datepicker-next").empty().append('<i class="fa fa-chevron-right _fix"></i>')
        }
    
    });

      $('#isWaiver, #isExemptedGoods, #newItemIndicator, #adjustmentIndicator, #othersIndicator, #newDateIndicator').checkToggler({
      labelOn: "YES",
      labelOff: "NO"
    }); 
      
    $('#billingIsDelivery').checkToggler({
      labelOn: "YES",
      labelOff: "NO"
    });
    
    $('#siteBriefMandatory').checkToggler({
      labelOn: "YES",
      labelOff: "NO"
    });
 
   $('.apply-textarea').textcounter({
      max: 500,
      countDowwn: true,
      type: "character",
      counterText:    "Characters Entered: %d",
      countSpaces: true
    });
    
    $('.text-count-message').append('<span class="maz">Max: 500</span>');
    
    $('.flexdatalist').flexdatalist({
      minLength: 1
    });
    
    $('._committee .nice-select .list .option:last-child').addClass('_create');
      
    $('._second-x').click(function(){
      $('._second-select').fadeOut();
    });
    
    $('._third-x').click(function(){
      $('._third-select').fadeOut();
    });
    
  });
  
   $(document).ready(function() {
      $(window).scroll(function(){
       if ($(this).scrollTop() > 500) {
          $('._xPos').show(500);
        } else {
          $('._xPos').hide(500);
        }
      });
    });
  
  $(document).ready(function(){
     $(window).scroll(function(){
      if ($(window).scrollTop() > 100) {
         $('.below-header').addClass('sticky-header');
         $('.search-header').addClass('sticky-search-header');
         $('.mobile-menu').addClass('sticky-header');
      }
      else {
         $('.below-header').removeClass('sticky-header');
         $('.search-header').removeClass('sticky-search-header');
         $('.mobile-menu').removeClass('sticky-header');
      }
    });
  });
  
  $(document).ready(function(){
    $('.mobile-search').click(function () {
        var flag = document.getElementById('mobile-search-bar').getAttribute('data-visible');

        if (flag == 'true') {
            $('.search-header').slideUp(100);
            $('.search-header').removeClass(activeClass);
            $('.search-header').addClass(nonActiveClass);
            $('.side-container').removeClass('side-container-search');

            document.getElementById('mobile-search-bar').setAttribute('data-visible', 'false');
        } else {
            $('.search-header').slideDown(100);
            $('.search-header').removeClass(nonActiveClass);
            $('.search-header').addClass(activeClass);
            $('.side-container').addClass('side-container-search');

            document.getElementById('mobile-search-bar').setAttribute('data-visible', 'true');
        }

        $('#ms-icon').toggleClass("fa-search fa-times");
    });
   
      $('.search-bar').click(function () {
          var flag = document.getElementById('search-bar').getAttribute('data-visible');

          if (flag == 'true') {
              $('.search-header').slideUp(100);
              $('.search-header').removeClass(activeClass);
              $('.search-header').addClass(nonActiveClass);

              document.getElementById('search-bar').setAttribute('data-visible', 'false');

          } else {
              $('.search-header').slideDown(100);
              $('.search-header').removeClass(nonActiveClass);
              $('.search-header').addClass(activeClass);

              document.getElementById('search-bar').setAttribute('data-visible', 'true');

          }

          $('#s-icon').toggleClass("fa-search fa-times");
      }); 
  });
  
  $( function() {
    var dateFormat = "dd/mm/yyyy",
      from = $( "#from" )
        .datepicker({
          defaultDate: "+1w",
          changeMonth: false,
          numberOfMonths: 1,
          dateFormat: 'dd/mm/yy',
          afterShow: function() {
        	$(".ui-datepicker-prev").empty().append('<i class="fa fa-chevron-left _fix"></i>')
          $(".ui-datepicker-next").empty().append('<i class="fa fa-chevron-right _fix"></i>')
          }
        }),
      to = $( "#to" ).datepicker({
        defaultDate: "+1w",
        changeMonth: false,
        numberOfMonths: 1,
        dateFormat: 'dd/mm/yy',
         afterShow: function() {
        	$(".ui-datepicker-prev").empty().append('<i class="fa fa-chevron-left _fix"></i>')
          $(".ui-datepicker-next").empty().append('<i class="fa fa-chevron-right _fix"></i>')
        }
      })
 
    function getDate( element ) {
      var date;
      try {
        date = $.datepicker.parseDate( dateFormat, element.value );
      } catch( error ) {
        date = null;
      }
      return date;
    }
  });
  
  $.datepicker._updateDatepicker_original = $.datepicker._updateDatepicker;
  $.datepicker._updateDatepicker = function(inst) {
      $.datepicker._updateDatepicker_original(inst);
      var afterShow = this._get(inst, 'afterShow');
      if (afterShow)
          afterShow.apply((inst.input ? inst.input[0] : null)); 
    }

    var mWidth = 414;
    $(document).ready(function () {
        if ($(window).width() <= mWidth) {
            $("#bids-accord").accordion({
                collapsible: true,
                active: false
            });
        }
    });

    $(function () {
        var icons = {
            header: "ui-icon-down",
            activeHeader: "ui-icon-up"
        };
        $("#bids-accord").accordion({
            collapsible: true,
            icons: icons
        });
    });

    $(document).ready(function () {
        $(".mCustomScrollbar").mCustomScrollbar({
            axis: "x",
            theme: "minimal-dark",
            alwaysShowScrollbar: 2,
            scrollInertia: 100
        });
    });
  
  /*$(document).mouseup(function(e){
    var count = 0;
    var container = $('.drops-notif');
      if (!container.is(e.target) && container.has(e.target).length === 0) {
       container.fadeOut();
      }
  });*/
})(jQuery);