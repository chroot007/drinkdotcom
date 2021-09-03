var cartItems = [];

$(document).ready(function () {
    $(".datalink").click(function () {

        var href = $(this).attr("data-href");

        if (href) {
            window.location.href = href;
        }
    });

    $(".btnAddToCart").click(function () {
        var $btn = $(this);

        var $inputField = $btn.parents(".itemCartHolder").find("input.productQuantity");

        var quantity = $inputField.val();

        if (quantity === undefined || quantity === null || quantity === 0 || quantity < 0 || quantity > 100) {
            quantity = 1;
            $inputField.val(quantity);
        }

        addItemToCart($btn.attr("data-id"), quantity);

        updateCartInfo();
    });

    $("[type='number'].productQuantity").keypress(function (evt) {
        if (!$(this).hasClass("ismobile")) {
            evt.preventDefault();
        }
    });
    
    InitCart();
    updateCartInfo();

    $(".cartMenu").click(function () {
        $(".bloader", "#cartModal").show();
        $(".cartItems", "#cartModal").html("");

        $.ajax({
            url: $("#cartProducts").val(),
            data: {
                productIDs : cartItems.join("-")
            }
        })
        .done(function (response) {
            $(".bloader", "#cartModal").hide();
            $(".cartItems", "#cartModal").html(response);
        })
        .fail(function () {
            swal("Error!", "Error Occured. Please try again later.", "error");
        });
    });

    $(".link-button").click(function () {
        var url = $(this).attr("data-href");

        if (url) {
            window.location.href = url;
        }
    });
});

//------- Price Range slider -------//
function applyPriceRange(rangeMin, rangeMax, startMin, startMax) {
    var nonLinearSlider = $('.price-range', "#price-widget")[0];

    noUiSlider.create(nonLinearSlider, {
        connect: true,
        behaviour: 'tap',
        start: [startMin, startMax],
        range: {
            'min': [rangeMin],
            'max': [rangeMax]
        }
    });

    var nodes = [
        $('.lower-value', "#price-widget")[0], // 0
        $('.upper-value', "#price-widget")[0]  // 1
    ];

    // Display the slider value and how far the handle moved
    // from the left edge of the slider.
    nonLinearSlider.noUiSlider.on('update', function (values, handle, unencoded, isTap, positions) {
        nodes[handle].innerHTML = values[handle];

        doSearch(values[0], values[1]);
    });
}

var searchTimeout;
function doSearch(priceMin, priceMax) {
    clearTimeout(searchTimeout);

    var searchURL = $("#searchURL").val();
    if (searchURL) {
        searchTimeout = setTimeout(function () {
            var url = "";

            if ($("#q").val()) {
                url = url + (url === "" ? "?" : "&") + "q=" + $("#q").val();
            }
            if (priceMin) {
                url = url + (url === "" ? "?" : "&") + "from=" + priceMin;
            }
            if (priceMax) {
                url = url + (url === "" ? "?" : "&") + "to=" + priceMax;
            }
            if ($("#pageSize").val()) {
                url = url + (url === "" ? "?" : "&") + "pageSize=" + $("#pageSize").val();
            }
            if ($("#sortBy").val()) {
                url = url + (url === "" ? "?" : "&") + "sortby=" + $("#sortBy").val();
            }

            window.location.href = searchURL + url;
            console.log("t end");
        }, 1000);
    }
}

function applyQuanityChanger() {
    $("[type='number'].changeQuantity").change(function (evt) {
        updateCartItems();
    });
}

function updateCartItems() {
    cartItems = [];

    $.each($("[type='number'].changeQuantity"), function (index, value) {
        var id = $(this).attr("data-id");
        var quantity = $(this).val();

        for (i = 0; i < quantity; i++) {
            cartItems.push(id);
        }
    });

    $.cookie('cartItems', cartItems.join("-"), { expires: 7, path: '/' });

    updateCartInfo();

    if (cartItems.length === 0) {
        window.location.reload();
    }
}

function addItemToCart(itemID, quantity) {
    for (i = 0; i < quantity; i++) {
        cartItems.push(itemID);
    }

    $.cookie('cartItems', cartItems.join("-"), { expires: 7, path: '/' });
}

function emptyCart() {
    cartItems = [];

    $.cookie('cartItems', cartItems.join("-"), { expires: 7, path: '/' });

    updateCartInfo();
}

function InitCart() {
    var items = $.cookie('cartItems');

    if (items !== undefined && items !== "") {
        cartItems = items.split("-");
    }    
}

function updateCartInfo() {
    $(".countholder", ".cartMenu").html(cartItems.length);

    $(".cartMenu").effect("bounce", { times: 3 }, 500);
}

function showFormOnClick() {
    $(".showForm").off("click");
    $(".showForm").click(function () {
        var modalID = $(this).attr("data-target");
        $(".floader", modalID).show();
        $(".response-holder", modalID).html("");

        $.ajax({
            url: $(this).attr("data-href"),
            type: "get"
        })
        .done(function (response) {
            $(".floader", modalID).hide();
            $(".response-holder", modalID).html(response);
        });
    });
}

function validateLoginForm() {
    $("#loginForm").validate({
        errorClass: "alert alert-danger mb-0",
        errorElement: "div",
        rules: {
            Username: {
                required: true
            },
            Password: {
                required: true
            },
            Recaptcha: {
                required: true
            }
        },
        messages: {
            Username: {
                required: "Please enter your accout username."
            },
            Password: {
                required: "Please enter your account password."
            },
            Recaptcha: {
                required: "Please confirm that you are human!"
            }
        },
        highlight: function (element, errorClass) {
            $(element).removeClass(errorClass);
        }
    });
}

function validateRegisterForm() {
    $("#registerForm").validate({
        errorClass: "alert alert-danger mb-0",
        errorElement: "div",
        rules: {
            FullName: {
                required: true
            },
            Username: {
                required: true
            },
            Email: {
                required: true,
                email: true
            },
            Password: {
                required: true
            },
            ConfirmPassword: {
                required: true,
                equalTo: "#regPassword"
            },
            Dateofbirth: {
                required: true,
            }
        },
        messages: {
            FullName: {
                required: "Please write your full name."
            },
            Username: {
                required: "Please write a unique username."
            },
            Email: {
                required: "Please enter a valid email address.",
                email: "Please enter a valid email address."
            },
            Password: {
                required: "Please enter password."
            },
            ConfirmPassword: {
                required: "Please confirm your password.",
                equalTo: "Passwords not matched."
            },
            Dateofbirth: {
                required: "Please enter a valid date of birth."
            }
        },
        highlight: function (element, errorClass) {
            $(element).removeClass(errorClass);
        }
    });
}

function validateForgotPasswordForm() {
    $("#forgotPasswordForm").validate({
        errorClass: "alert alert-danger mb-0",
        errorElement: "div",
        rules: {
            Username: {
                required: true
            }
        },
        messages: {
            Username: {
                required: "Please enter your account username."
            }
        },
        highlight: function (element, errorClass) {
            $(element).removeClass(errorClass);
        }
    });
}


function validateResetPasswordForm() {
    $("#resetPasswordForm").validate({
        errorClass: "alert alert-danger mb-0",
        errorElement: "div",
        rules: {
            Password: {
                required: true
            },
            ConfirmPassword: {
                required: true,
                equalTo: "#regPassword"
            }
        },
        messages: {
            Password: {
                required: "Please enter password."
            },
            ConfirmPassword: {
                required: "Please confirm your password.",
                equalTo: "Passwords not matched."
            }
        },
        highlight: function (element, errorClass) {
            $(element).removeClass(errorClass);
        }
    });
}

function PrintElem(elem) {
    var mywindow = window.open('', 'PRINT', 'height=400,width=600');

    mywindow.document.write('<html><head><title>' + document.title + '</title>');
    mywindow.document.write('<link href="/Content/vendor/bootstrap/css/bootstrap.min.css" rel="stylesheet" />');
    mywindow.document.write('<script src="/Content/vendor/jquery/jquery.min.js">');
    mywindow.document.write('</sc' + 'ript>');
    mywindow.document.write('<script src="/Content/vendor/bootstrap/js/bootstrap.min.js">');
    mywindow.document.write('</sc' + 'ript>');

    mywindow.document.write('</head><body >');
    mywindow.document.write('<h1>' + document.title + '</h1>');
    mywindow.document.write(document.getElementById(elem).innerHTML);
    mywindow.document.write('</body></html>');

    mywindow.document.close(); // necessary for IE >= 10
    mywindow.focus(); // necessary for IE >= 10*/

    mywindow.print();
    mywindow.close();

    return true;
}