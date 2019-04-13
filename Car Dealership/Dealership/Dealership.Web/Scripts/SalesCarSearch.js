$(document).ready(function () {
    $(".btn.btn-primary").click(function () {
        if ($(this).text().search(new RegExp("purchase", "i")) > -1) {
            var vehicleID = $(this).attr("id")
            createCookie("VehicleId", VehicleId, "1800000", "/")
        }
    });
    salescarsearch();
});
function createCookie(name, value, ms, path) {
    if (!name || !value) {
        return;
    }
    var d;
    var cpath = path ? '; path=' + path : '';
    var expires = '';
    if (ms) {
        d = new Date();
        d.setTime(d.getTime() + ms);
        expires = '; expires=' + d.toUTCString();
    }
    document.cookie = name + "=" + value + expires + cpath;
}

var url = "http://localhost:50822";

function fail(response) {
    console.log("Failed!", response);
}

function salescarsearch() {
    $('#results').show();
    $('#searchoptions').show();
    $('#cardetails').hide();
    $('#customerform').hide();

    $('#adminsearch').on('click', function () {
        var minPrice = $('#minpricevalue').val();
        var maxPrice = $('#maxpricevalue').val();
        var minYear = $('#minyearvalue').val();
        var maxYear = $('#maxyearvalue').val();
        var search = $('#searchbar').val();

        if (search == "") {
            search = "empty"
        }

        //alert('Ok');
        $.ajax({
            type: 'GET',
            url: url + "/Admin/Vehicles/" + search + "/" + minPrice + "/" + maxPrice + "/" + minYear + "/" + maxYear,
            success: function (carDisplay) {
                //alert('Ok');

                var resultsDiv = $('#results');
                '<br />'
                $.each(carDisplay, function (index, car) {

                    var carInfo =
                        //Divided code into parts, I hate when it runs to the sides :P
                        //left column
                        '<div class="col-md-12"><div class="row"><div class="col-md-3"><p>' + car.Year + " " + " " + car.ModelName.MakeName.Make
                        + " " + car.ModelName.Model + '</p>' + '<p>' + '<img src ="' + car.Image + '"class="imageSize" />'
                        + '</p></div>'

                        //middle left column
                        + '<div class="col-md-3"><p></p>' + '<p>' + "Body Style: " + car.BodyName.BodyType + '</p>' + '<p>' +
                        "Transmission: " + car.TransmissionType.TransmissionName + '</p>' + '<p>' + "Color: " + car.Color + '</p>' + '</div>'

                        //middle right column
                        + '<div class="col-md-3"><p></p>' + '<p>' + "Interior: " + car.Interior + '</p>' + '<p>' + "Mileage: "
                        + car.Mileage + '</p>' + '<p>' + "VIN: " + car.VIN + '</p></div>'

                        //right column
                        + '<div class="col-md-3"><p></p>' + '<p>' + "Sale Price: $" + car.SalePrice + '</p>' + '<p>' + "MSRP: $"
                        + car.MSRP + '</p>' + '<p><a href="/Sales/Purchase/' + car.VehicleId + '"><button type="submit" class="btn btn-primary" id="purchasebutton">Purchase</button></a></p></div></div>'

                    //description row
                    '<div class="col-md-12">' + car.Description + '</div>'

                    //closing tag for entire div
                    '</div>'

                    resultsDiv.append(carInfo);
                })
            },

            error: function () {
                alert('FAILURE!');
            }

        });
    });
}

function VehicleEdit(id) {
    $('#results').show();
    $('#searchoptions').hide();
    $('#adminsearch').hide();
    $('#cardetails').show();
    $('#customerform').show();
    $.ajax({
        type: 'GET',
        url: url + "/Admin/EditVehicle/" + id,
        success: function (car) {

                
        },

        error: function () {
            alert('Details!');
        }

    });
}