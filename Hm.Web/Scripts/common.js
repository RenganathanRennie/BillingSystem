var newList = new Array();
newList['files'] = new Array();
newList['headerName'] = new Array();
var validDocFile = /(\.pdf|\.xls|\.xlsx|\.doc|\.docx)$/i;
var validImageFile = /(\.jpg|\.jpeg|\.bmp|\.gif|\.png)$/i;


(function ($) {
    $.fn.uploadAttach = function (action) {
        var uploadCtrl = $(this);
        var formData = new FormData();
        if (action === "document") {         
            $(this).on('change', function () {
                if ($("#attach_document").val().trim() == "") {
                    $("#fileHeaderError").fadeIn();
                    $(uploadCtrl).val("");
                    return;
                }
                else {
                    $("#fileHeaderError").fadeOut();
                }
                var files = $(this).get(0).files;
                for (var i = 0; i < files.length; i++) {
                    if (!validDocFile.exec(files.item(i).name)) {
                        $("#fileHeaderError").html("Only 'pdf','xls','xlsx','doc','docx','jpg','jpeg','png'");
                        $("#fileHeaderError").fadeIn();
                        return;
                    }
                    newList['files'].push(files.item(i));
                    newList['headerName'].push($("#txtHeaderAttach").val().trim());
                }
                populateGrid();
                $(uploadCtrl).val("");
                $(uploadCtrl).prev().val("");
                formData.append('files', newList);
            });
        }
        if (action === "image") {           
            $(this).change(function () {
                if (!validImageFile.exec($(this).get(0).files[0].name)) {
                    $(this).val("");
                    alert("Upload Only 'jpg','jpeg','bmp','gif','png'");
                    return;
                }
                var reader = new FileReader();
                reader.onload = function (e) {
                    $('#imagePrev').attr('src', e.target.result);
                };
                reader.onerror = function (event) {
                    alert("File could not be read! Code " + event.target.error.code);
                };
                reader.readAsDataURL($(this).get(0).files[0]);
                $('#imagePrev').css('display', 'none');
                $('#imagePrev').fadeIn(2000);
            });
        }
        function populateGrid() {
            var textFile = "";
            for (var j = 0; j < newList['files'].length; j++) {
                textFile += "<ul><li class='fileHeade'>" + newList['files'][j].name + "</li><li class='fileName'>" + newList['headerName'][j] + "</li><li class='removefile' data-index='" + j + "'>Remove</li></ul>";
            }
            $("#uploadedfile").html('');
            $("#uploadedfile").append(textFile);         
            $('.removefile').click(function () {
                newList['files'].splice($(this).data('index'), 1);
                newList['headerName'].splice($(this).data('index'), 1);
                populateGrid();
            })
        }

    };

}(jQuery));

function OpenPopup(e, t, n, r, i, s) { s = typeof a !== "undefined" ? s : false; var o = new Date; var u = o.toString().split(" ").join("").split(":").join("").split("+").join(""); var f = encodeURI("DT=" + u); winheight = "600px"; if (i == "Contra" && i == "Credit" && i == "Debit" && i == "Journal" && i == "Employee") { set_position = "top" } else { set_position = "center" } $(e).click(function () { OpenDialog = $(t); OpenDialog.dialog({ autoOpen: s, width: r, height: winheight, title: i, modal: true, position: set_position, closeOnEscape: true, resizable: false, draggable: false, open: function (e, t) { }, close: function (e, t) { window.location = window.location.pathname } }); n = n; $("#loader").show(); OpenDialog.load(n, function () { $("#loader").hide(); OpenDialog.dialog("open") }) }) }
function OpenPopupInsTance(e, t, n, r, i, s) {
    s = typeof a !== "undefined" ? s : false;
    var o = new Date;
    var u = o.toString().split(" ").join("").split(":").join("").split("+").join("");
    var f = encodeURI("DT=" + u);
    winheight = "600px";
    if (i == "Contra" && i == "Credit" && i == "Debit" && i == "Journal" && i == "Employee") {
        set_position = "top"
    }
    else {
        set_position = "center"
    }
    OpenDialog = $(t);
    OpenDialog.dialog(
        {
            autoOpen: s,
            width: r,
            height: winheight,
            title: i,
            modal: true,
            position: set_position,
            closeOnEscape: true,
            resizable: true,
            draggable: false,
            open: function (e, t) { },
            close: function (e, t) { window.location = window.location.pathname }
        });
    n = n;
    $("#loader").show();
    OpenDialog.load(n, function () {
        $("#loader").hide(); OpenDialog.dialog("open")
    })
} function showdatetimepicker(e, t, n, r) { var i = ""; var s = ""; if (n == "mindate") { i = new Date; s = "" } if (n == "maxdate") { s = new Date; i = "" } if (n == "setfinancialyear") { s = new Date; i = new Date("2013-Jan-01") } if (t == "yes") { $(e).datetimepicker({ showOn: "button", buttonImage: "../images/icon_calendar.png", buttonImageOnly: true, dateFormat: "dd/mm/yy", changeMonth: true, changeYear: true }) } else { if (r == "yes") { $(e).datepicker({ changeMonth: true, changeYear: true, showOn: "button", buttonImage: "../images/icon_calendar.png", buttonImageOnly: true, dateFormat: "dd/mm/yy", changeMonth: true, changeYear: true }) } else { $(e).datepicker({ changeMonth: true, changeYear: true, showOn: "button", buttonImage: "../images/icon_calendar.png", buttonImageOnly: true, dateFormat: "dd/mm/yy", changeMonth: true, changeYear: true }) } } } function showdatetimepicker(e, t, n, r) { var i = ""; var s = ""; if (n == "mindate") { i = new Date; s = "" } if (n == "maxdate") { s = new Date; i = "" } if (n == "setfinancialyear") { s = new Date; i = new Date("2013-Jan-01") } if (t == "yes") { $(e).datetimepicker({ showOn: "button", buttonImage: "../images/icon_calendar.png", buttonImageOnly: true, dateFormat: "dd/mm/yy", changeMonth: true, changeYear: true }) } else { if (r == "yes") { $(e).datepicker({ showOn: "button", changeMonth: true, changeYear: true, buttonImage: "../images/icon_calendar.png", buttonImageOnly: true, dateFormat: "dd/mm/yy", changeMonth: true, changeYear: true }) } else { $(e).datepicker({ showOn: "button", changeMonth: true, changeYear: true, buttonImage: "../images/icon_calendar.png", buttonImageOnly: true, dateFormat: "dd/mm/yy", changeMonth: true, changeYear: true }) } } } function loadPartialUI(e, t) { $("#loader").show(); $("#add_or_edit").show(); $.get(e, function (e) { $(t).html(e); $("#loader").hide() }) }


function exportExcel() {
    var URL = expoetExcelUrl + "?funcName=" + curFunction;
    $.ajax({
        url: URL,
        type: "post", dataType: "json",
        data: { funcName: curFunction },
        error: function (n) { window.location.href = URL },
        success: function (n) {
            if (!n.status) {
                alert(n.msg);
                return;
            }
        }
    })
}


function exportExcelArgs(curFunction) {
    var URL = expoetExcelUrl + "?funcName=" + curFunction;
    $.ajax({
        url: URL,
        type: "post", dataType: "json",
        data: { funcName: curFunction },
        error: function (n) { window.location.href = URL },
        success: function (n) {
            if (!n.status) {
                alert(n.msg);
                return;
            }
        }
    })
}


function ShowInfo(e) {
    window.open(e)
}

function autoComplete(htmlObject, sourceUrl, cacheDistinct) {
    $(htmlObject).autocomplete({
        minLength: 0,
        source: function (request, response) {
            var term = request.term;
            if (term in cacheDistinct) {
                response(cacheDistinct[term]);
                return;
            }

            $.getJSON(sourceUrl, request, function (data) {
                cacheDistinct[term] = data;
                response(data);
            });
        }
    }).bind('focus click', function () { $(this).val("").autocomplete("search"); });
}

function hideCreate(divId) {
    $("#" + divId).fadeOut();
}



(function ($) {
    $.fn.postAjax = function (options) {

        var formDatas = new Array();

        $.fn.postAjax.defaults = {           
            ajaxCompleted: function (data) {

                if (data.status == 1) {
                    alert(data.output);
                    window.location.reload();
                }
                else {
                    $("#loader").hide();
                    $(this).parent().html(data.output);
                }

            },
            appendMore: function (data) {
                return data;
            }
        };


        options = $.extend($.fn.postAjax.defaults, options);      

        this.bind("submit.postAjax", function (e) {          
            e.preventDefault();
            $("form").removeData("validator").removeData("unobtrusiveValidation");
            $.validator.unobtrusive.parse("form");
            formDatas = $(this).serializeArray();
            formDatas = options.appendMore(formDatas);     


            var formData = new FormData();
            var model = formDatas;          
            $.each(model, function (key, input) {
                formData.append(input.name, input.value);
            });

            if ($("#StaffImage").get(0) && $("#StaffImage").get(0).files.length > 0) {
                formData.append('StaffImage', $("#StaffImage").get(0).files[0]);
            }

            for (var k = 0; k < newList['files'].length; k++) {
                formData.append('attachFiles' + k, newList['files'][k]);
                formData.append('HeaderName[' + newList['files'][k].name + ']', newList['headerName']);
            }


            if ($(this).valid()) {
                $.ajax({
                    url: $(this).attr('action'),
                    type: "post",
                    dataType: 'json',
                    data: formData,
                    processData: false,
                    contentType: false,
                    error: function (data) {
                        $("#loader").hide();
                        alert("Network error occured please reload the page.")
                    },
                    success: function (data) {
                        console.log(options.test);
                        options.ajaxCompleted(data);
                    }
                });
            }
        });       

    };
}(jQuery));


(function ($) {
    $.fn.populateAjax = function () {
        this.bind("click.populateAjax", function (e) {            
            defaultRoute = $("#tabs").find("#ui-id-3").attr('href');
            e.preventDefault();
            routeUrl = $(this).data("action");         
            $("#tabs").find("#ui-id-3").attr('href', routeUrl);
            $("#tabs").tabs({ active: 1, cache: true, });
            $("#tabs").find("#ui-id-3").attr('href', defaultRoute);
        });
    };
}(jQuery));

(function ($) {
    $.fn.searchAjax = function () {
        this.bind("submit.searchAjax", function (e) {         
            e.preventDefault();
            routeUrl = $(this).attr("action") + "?" + $(this).serialize();          
            $("#tabs").find("#ui-id-1").attr('href', routeUrl);
            $("#tabs").tabs('load', 0);           
        });
    };
}(jQuery));

(function ($) {
    $.fn.searchPaging = function () {
        this.bind("click.searchPaging", function (e) {         
            e.preventDefault();
            routeUrl = $(this).attr('href');      
            $("#tabs").find("#ui-id-1").attr('href', routeUrl);
            $("#tabs").tabs('load', 0);
        });
    };
}(jQuery));

$(document).ready(function () {

    if (typeof $.validator !== "undefined")
        $.validator.setDefaults({ ignore: [] });


    $("#btnUserClear").click(function () {
        $(':input', "#user_detail_form form")
            .not(':button, :submit, :reset, :hidden')
            .val('')
            .removeAttr('checked')
            .removeAttr('selected');
        if ($(".create_or_update").val() == "" || $(".create_or_update").val() == "0") {
            $('#imagePrev').attr('src', '');
        }

    })


    $("body").delegate(".delete-resource", "click", function (e) {
        curRes = $(this);
        requestURL = curRes.data('action');
      
        $.post(requestURL, {}, function (data) {
            if (data.status) {
                alert(data.output);
                curRes.parent().parent().parent().remove();
            }
        }, "json")

    });

    $("body").delegate(".qty-check", "blur", function (e) {     
        if (globalQTYDecimal) {
            var value = parseFloat($(this).val());
            if (!isNaN(value))
                $(this).val(value.toFixed(globalQTYDecimal));
        }

    });

    $("body").delegate(".price-check", "blur", function (e) {       
        if (globalPriceDecimal) {
            var value = parseFloat($(this).val());
            if (!isNaN(value))
                $(this).val(value.toFixed(globalPriceDecimal));

        }
    });


});


