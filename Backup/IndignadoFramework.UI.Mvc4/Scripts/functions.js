$(function() {
	$('.field, textarea').focus(function() {
        if(this.title==this.value) {
            this.value = '';
        }
    }).blur(function(){
        if(this.value=='') {
            this.value = this.title;
        }
    });
      
    $('.flexslider').flexslider({
        animation: "fade",
        slideshow: true,
        controlNav: true,
        directionNav: false              
    });  

    $('.videos-slider').jcarousel({
    	scroll: 1,
    	auto: 3,
    	wrap: 'both',
    	buttonNextHTML: null,
        buttonPrevHTML: null,
        initCallback: secondarySlider
    });

    $('.galeria-slider').jcarousel({
        scroll: 1,
        auto: 5,
        wrap: 'both',
        buttonNextHTML: null,
        buttonPrevHTML: null,
    });

    $('.videos-slider li').hover( function(){
    	$(this).addClass('active');
    }, function() {
    	$(this).removeClass('active');
    });

 $('.galeria-slider li').hover(function () {
     $(this).addClass('active');
 }, function () {
     $(this).removeClass('active');
 });

    $(".videos-slider li").click(function(){
      window.location=$(this).find("a").attr("href");
      return false;
    });

    if ($.browser.msie && $.browser.version == 6) {
		DD_belatedPNG.fix('#wrapper, #logo a, .socials a, #search, #navigation, #navigation a, #slider, #slider img, .caption, a.watch-now, #main-top, #main-middle, #main-bottom, a.learn-more, .videos-slider, .small-caption, .ant-flecha, .sig-flecha, .flex-control-nav a, .shell');
	}
});

function secondarySlider(carousel) {
   $('.sig-flecha').bind('click', function() {
        carousel.next();
        return false;
    });

    $('.ant-flecha').bind('click', function() {
        carousel.prev();
        return false;
    });
}

function searchFailed() {
    $("#resultadosbusqueda").html("Upps.. hubo un problema.");
}