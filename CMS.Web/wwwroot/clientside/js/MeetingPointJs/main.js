/*
 *****************************************************
 *	CUSTOM JS DOCUMENT                              *
 *	Single window load event                        *
 *   "use strict" mode on                            *
 *****************************************************
 */
$(window).on("load", function() {

    "use strict";

    var preLoader = $('.preloader');
    var fancybox = $('.fancybox');
    var faq = $('#faq');
    var comingSoonTimer = $('#coming-soon-timer');
    var fancyboxIframe = $('.fancybox-iframe');
    var searchToggl = $('#searchtoggl');
    var searchLink = $('#searchtoggl i');
    var searchBar = $('#searchbar');
    var dataToggleTooTip = $('[data-toggle="tooltip"]');
    var progressBar = $(".progress-bar");

    // ============================================
    // PreLoader On window Load
    // =============================================
    if (preLoader.length) {
        preLoader.addClass('loaderout');
    }


    // ============================================
    // Search Bar Effect
    // =============================================

    searchToggl.on('click', function(e) {
        e.preventDefault();

        if ($(this).attr('id') == 'searchtoggl') {
            if (!searchBar.is(":visible")) {
                // if invisible we switch the icon to appear collapsable
                searchLink.removeClass('fa-search').addClass('fa-search-minus');
            } else {
                // if visible we switch the icon to appear as a toggle
                searchLink.removeClass('fa-search-minus').addClass('fa-search-plus');
            }

            searchBar.slideToggle(300, function() {
                // callback after search bar animation
            });
        }

    });

    //========================================
    // LightBox / Fancybox
    //======================================== 	

    if (fancybox.length) {
        fancybox.fancybox();
    }


    if (fancyboxIframe.length) {
        fancyboxIframe.fancybox({
            type: "iframe",
            // other API options
        });
    }

    //========================================
    // Skills Progess Bar Setting 
    //======================================== 	



    dataToggleTooTip.tooltip({
        trigger: 'manual'
    }).tooltip('show');

    progressBar.each(function() {
        var each_bar_width = $(this).attr('aria-valuenow');
        $(this).width(each_bar_width + '%');
    });

    //========================================
    // Accordion functions Calling
    //======================================== 	

    if (faq.length) {
        faq.accordion();
    }


    //========================================
    // Owl Carousel functions Calling
    //======================================== 	

    owlCarouselInit();

    //========================================
    // Comming Soon Timer function Calling
    //======================================== 

    if (comingSoonTimer.length) {
        comingsoonInt();
    }


});



//========================================
// Owl Carousel functions
//======================================== 	

function owlCarouselInit() {

    "use strict";

    //========================================
    // owl carousels settings
    //======================================== 		
    var mainSlider = $('#main-slider');
    var aboutSlider = $('#about-us-slider');
    var facilitiesSlider = $('#facilities-slider');
    var blogSlider = $('#blog-sliders');
    var blogSliderSm = $('#blog-slider-sm');
    var testimonialS = $('#testimonial');
    var partenerSlider = $('#partener-slider');
    var twitterFeed = $('#twitter-feeds');
    var nextNav = 'Next';
    var prevNav = 'Prev';

    if (mainSlider.length) {
        mainSlider.owlCarousel({
            loop: true,
            margin: 0,
            nav: true,
            navText: [prevNav, nextNav],
            dots: false,
            autoplay: false,
            responsive: {
                0: {
                    items: 1
                },
                600: {
                    items: 1
                },
                1000: {
                    items: 1
                }
            }
        });
    }

    if (aboutSlider.length) {
        aboutSlider.owlCarousel({
            loop: true,
            margin: 0,
            nav: true,
            navText: [prevNav, nextNav],
            dots: false,
            autoplay: true,
            responsive: {
                0: {
                    items: 1
                },
                600: {
                    items: 1
                },
                1000: {
                    items: 1
                }
            }
        });

    }

    if (facilitiesSlider.length) {
        facilitiesSlider.owlCarousel({
            loop: true,
            margin: 0,
            nav: false,
            dots: true,
            autoplay: true,
            autoplayTimeout: 1500,
            stopOnHover: false,
            responsive: {
                0: {
                    items: 1
                },
                600: {
                    items: 1
                },
                1000: {
                    items: 1
                }
            }
        });

    }

    if (blogSlider.length) {
        blogSlider.owlCarousel({
            loop: true,
            margin: 0,
            nav: false,
            navText: [prevNav, nextNav],
            dots: true,
            autoplay: true,
            responsive: {
                0: {
                    items: 1
                },
                600: {
                    items: 1
                },
                1000: {
                    items: 1
                }
            }
        });

    }

    if (blogSliderSm.length) {
        blogSliderSm.owlCarousel({
            loop: true,
            margin: 0,
            nav: true,
            navText: [prevNav, nextNav],
            dots: false,
            autoplay: true,
            responsive: {
                0: {
                    items: 1
                },
                600: {
                    items: 1
                },
                1000: {
                    items: 1
                }
            }
        });

    }

    if (testimonialS.length) {
        testimonialS.owlCarousel({
            loop: true,
            margin: 0,
            nav: true,
            navText: [prevNav, nextNav],
            dots: false,
            autoplay: false,
            responsive: {
                0: {
                    items: 1
                },
                600: {
                    items: 1
                },
                1000: {
                    items: 1
                }
            }
        });

    }

    if (partenerSlider.length) {
        partenerSlider.owlCarousel({
            loop: true,
            margin: 0,
            nav: true,
            navText: [prevNav, nextNav],
            dots: false,
            autoplay: true,
            responsive: {
                0: {
                    items: 1
                },
                600: {
                    items: 3
                },
                1000: {
                    items: 4
                }
            }
        });

    }
	
    if (twitterFeed.length) {
        twitterFeed.owlCarousel({
            loop: true,
            margin: 0,
            nav: true,
            navText: [prevNav, nextNav],
            dots: false,
            autoplay: true,
            responsive: {
                0: {
                    items: 1
                },
                600: {
                    items: 1
                },
                1000: {
                    items: 1
                }
            }
        });

    }
}


//========================================
//Coming Soon Timer functions
//======================================== 	

function comingsoonInt() {

    "use strict";

    // Set the date we're counting down to
    var countDownDate = new Date("DEC 24, 2017 15:37:25").getTime();

    // Update the count down every 1 second
    var x = setInterval(function() {

        // Get todays date and time
        var now = new Date().getTime();

        // Find the distance between now an the count down date
        var distance = countDownDate - now;

        // Time calculations for days, hours, minutes and seconds
        var days = Math.floor(distance / (1000 * 60 * 60 * 24));
        var hours = Math.floor((distance % (1000 * 60 * 60 * 24)) / (1000 * 60 * 60));
        var minutes = Math.floor((distance % (1000 * 60 * 60)) / (1000 * 60));
        var seconds = Math.floor((distance % (1000 * 60)) / 1000);

        // Output the result in an element with id="demo"
        document.getElementById("days").innerHTML = days;
        document.getElementById("hours").innerHTML = hours;
        document.getElementById("seconds").innerHTML = seconds;
        document.getElementById("minutes").innerHTML = minutes;


    }, 1000);

}

/*
 *****************************************************
 *	END OF THE JS 									*
 *	DOCUMENT                       					*
 *****************************************************
 */