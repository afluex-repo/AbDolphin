$(function() {
	"use strict";
	new PerfectScrollbar(".app-container"),
	new PerfectScrollbar(".header-message-list"),
	new PerfectScrollbar(".header-notifications-list"),


	    $(".mobile-search-icon").on("click", function() {
			$(".search-bar").addClass("full-search-bar")
		}),

		$(".search-close").on("click", function() {
			$(".search-bar").removeClass("full-search-bar")
		}),

		$(".mobile-toggle-menu").on("click", function() {
			$(".wrapper").addClass("toggled")
		}),
		



		$(".dark-mode").on("click", function() {

			if($(".dark-mode-icon i").attr("class") == 'bx bx-sun') {
				$(".dark-mode-icon i").attr("class", "bx bx-moon");
				$("html").attr("class", "light-theme")
			} else {
				$(".dark-mode-icon i").attr("class", "bx bx-sun");
				$("html").attr("class", "dark-theme")
			}

		}), 

		
		$(".toggle-icon").click(function() {
			$(".wrapper").hasClass("toggled") ? ($(".wrapper").removeClass("toggled"), $(".sidebar-wrapper").unbind("hover")) : ($(".wrapper").addClass("toggled"), $(".sidebar-wrapper").hover(function() {
				$(".wrapper").addClass("sidebar-hovered")
			}, function() {
				$(".wrapper").removeClass("sidebar-hovered")
			}))
		}),
		$(document).ready(function() {
			$(window).on("scroll", function() {
				$(this).scrollTop() > 300 ? $(".back-to-top").fadeIn() : $(".back-to-top").fadeOut()
			}), $(".back-to-top").on("click", function() {
				return $("html, body").animate({
					scrollTop: 0
				}, 600), !1
			})
		}),
		
		$(function() {
			for (var e = window.location, o = $(".metismenu li a").filter(function() {
					return this.href == e
				}).addClass("").parent().addClass("mm-active"); o.is("li");) o = o.parent("").addClass("mm-show").parent("").addClass("mm-active")
		}),
		
		
		$(function() {
			$("#menu").metisMenu()
		}), 
		
		$(".chat-toggle-btn").on("click", function() {
			$(".chat-wrapper").toggleClass("chat-toggled")
		}), $(".chat-toggle-btn-mobile").on("click", function() {
			$(".chat-wrapper").removeClass("chat-toggled")
		}),


		$(".email-toggle-btn").on("click", function() {
			$(".email-wrapper").toggleClass("email-toggled")
		}), $(".email-toggle-btn-mobile").on("click", function() {
			$(".email-wrapper").removeClass("email-toggled")
		}), $(".compose-mail-btn").on("click", function() {
			$(".compose-mail-popup").show()
		}), $(".compose-mail-close").on("click", function() {
			$(".compose-mail-popup").hide()
		}), 
		
		
		$(".switcher-btn").on("click", function() {
			$(".switcher-wrapper").toggleClass("switcher-toggled")
		}), $(".close-switcher").on("click", function() {
			$(".switcher-wrapper").removeClass("switcher-toggled")
		}), $("#lightmode").on("click", function() {
			$("html").attr("class", "light-theme")
		}), $("#darkmode").on("click", function() {
			$("html").attr("class", "dark-theme")
		}), $("#semidark").on("click", function() {
			$("html").attr("class", "semi-dark")
		}), $("#minimaltheme").on("click", function() {
			$("html").attr("class", "minimal-theme")
		}), $("#headercolor1").on("click", function() {
			$("html").addClass("color-header headercolor1"), $("html").removeClass("headercolor2 headercolor3 headercolor4 headercolor5 headercolor6 headercolor7 headercolor8")
		}), $("#headercolor2").on("click", function() {
			$("html").addClass("color-header headercolor2"), $("html").removeClass("headercolor1 headercolor3 headercolor4 headercolor5 headercolor6 headercolor7 headercolor8")
		}), $("#headercolor3").on("click", function() {
			$("html").addClass("color-header headercolor3"), $("html").removeClass("headercolor1 headercolor2 headercolor4 headercolor5 headercolor6 headercolor7 headercolor8")
		}), $("#headercolor4").on("click", function() {
			$("html").addClass("color-header headercolor4"), $("html").removeClass("headercolor1 headercolor2 headercolor3 headercolor5 headercolor6 headercolor7 headercolor8")
		}), $("#headercolor5").on("click", function() {
			$("html").addClass("color-header headercolor5"), $("html").removeClass("headercolor1 headercolor2 headercolor4 headercolor3 headercolor6 headercolor7 headercolor8")
		}), $("#headercolor6").on("click", function() {
			$("html").addClass("color-header headercolor6"), $("html").removeClass("headercolor1 headercolor2 headercolor4 headercolor5 headercolor3 headercolor7 headercolor8")
		}), $("#headercolor7").on("click", function() {
			$("html").addClass("color-header headercolor7"), $("html").removeClass("headercolor1 headercolor2 headercolor4 headercolor5 headercolor6 headercolor3 headercolor8")
		}), $("#headercolor8").on("click", function() {
			$("html").addClass("color-header headercolor8"), $("html").removeClass("headercolor1 headercolor2 headercolor4 headercolor5 headercolor6 headercolor7 headercolor3")
		})
		
	// sidebar colors 
	$('#sidebarcolor1').click(theme1);
	$('#sidebarcolor2').click(theme2);
	$('#sidebarcolor3').click(theme3);
	$('#sidebarcolor4').click(theme4);
	$('#sidebarcolor5').click(theme5);
	$('#sidebarcolor6').click(theme6);
	$('#sidebarcolor7').click(theme7);
	$('#sidebarcolor8').click(theme8);

	function theme1() {
		$('html').attr('class', 'color-sidebar sidebarcolor1');
	}

	function theme2() {
		$('html').attr('class', 'color-sidebar sidebarcolor2');
	}

	function theme3() {
		$('html').attr('class', 'color-sidebar sidebarcolor3');
	}

	function theme4() {
		$('html').attr('class', 'color-sidebar sidebarcolor4');
	}

	function theme5() {
		$('html').attr('class', 'color-sidebar sidebarcolor5');
	}

	function theme6() {
		$('html').attr('class', 'color-sidebar sidebarcolor6');
	}

	function theme7() {
		$('html').attr('class', 'color-sidebar sidebarcolor7');
	}

	function theme8() {
		$('html').attr('class', 'color-sidebar sidebarcolor8');
	}
	






    //custom js start here

   

    


    // chart 17
	var ctx = document.getElementById("chart17").getContext('2d');

	var gradientStroke8 = ctx.createLinearGradient(0, 0, 0, 300);
	gradientStroke8.addColorStop(0, '#3366cc');
	gradientStroke8.addColorStop(1, '#3366cc');

	var gradientStroke9 = ctx.createLinearGradient(0, 0, 0, 300);
	gradientStroke9.addColorStop(0, '#ff9900');
	gradientStroke9.addColorStop(1, '#ff9900');


	var gradientStroke10 = ctx.createLinearGradient(0, 0, 0, 300);
	gradientStroke10.addColorStop(0, '#109618');
	gradientStroke10.addColorStop(1, '#109618');

	var myChart = new Chart(ctx, {
	    type: 'polarArea',
	    data: {
	        labels: ["Total", "InActive", "Active"],
	        datasets: [{
	            backgroundColor: [
                  gradientStroke8,
                  gradientStroke9,
                  gradientStroke10
	            ],

	            hoverBackgroundColor: [
                 gradientStroke8,
                 gradientStroke9,
                 gradientStroke10
	            ],
	            data: [5, 8, 7]
	        }]
	    },
	    options: {
	        maintainAspectRatio: false,
	        plugins: {
	            legend: {
	                display: false,
	                position: 'bottom'
	            }
	        }

	    }
	});


    // chart 18
	var ctx = document.getElementById("chart18").getContext('2d');

	var gradientStroke11 = ctx.createLinearGradient(0, 0, 0, 300);
	gradientStroke11.addColorStop(0, '#ba8b02');
	gradientStroke11.addColorStop(1, '#181818');

	var gradientStroke12 = ctx.createLinearGradient(0, 0, 0, 300);
	gradientStroke12.addColorStop(0, '#2c3e50');
	gradientStroke12.addColorStop(1, '#fd746c');


	var gradientStroke13 = ctx.createLinearGradient(0, 0, 0, 300);
	gradientStroke13.addColorStop(0, '#ff0099');
	gradientStroke13.addColorStop(1, '#493240');

	var myChart = new Chart(ctx, {
	    type: 'doughnut',
	    data: {
	        labels: ["Jeans", "T-Shirts", "Shoes"],
	        datasets: [{
	            backgroundColor: [
                  gradientStroke11,
                  gradientStroke12,
                  gradientStroke13
	            ],
	            hoverBackgroundColor: [
                  gradientStroke11,
                  gradientStroke12,
                  gradientStroke13
	            ],
	            data: [25, 25, 25]
	        }]
	    },
	    options: {
	        maintainAspectRatio: false,
	        plugins: {
	            legend: {
	                display: false,
	                position: 'bottom'
	            }
	        }

	    }
	});



    //custom js end here

});