$(document).ready(function () {
    admincarsearch();
});

var url = "http://localhost:50822";

function fail(response) {
    console.log("Failed!", response);
}

function admincarsearch() {
    $('#results').show();
    $('#searchoptions').show();
    $('#cardetails').hide();

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
                $('#results').empty();
                var resultsDiv = $('#results');
                '<br />'
                $.each(carDisplay, function (index, car) {

                    var carInfo =
                        //Divided code into parts, I hate when it runs to the sides :P
                        //left column
                        '<div class="col-md-12"><div class="row"><div class="col-md-3"><p>' + car.Year + " " + " " + car.ModelName.MakeName.Make
                        + " " + car.ModelName.Model + '</p>' + '<p>' + '<img src ="' + car.ImageUrl + '"class="imageSize" />'
                        + '</p></div>'

                        //middle left column
                        + '<div class="col-md-3"><p></p>' + '<p>' + "Body Style: " + car.BodyName.BodyType + '</p>' + '<p>' +
                        "Transmission: " + car.TransmissionType.TransmissionName + '</p>' + '<p>' + "Color: " + car.Color + '</p>' + '</div>'

                        //middle right column
                        + '<div class="col-md-3"><p></p>' + '<p>' + "Interior: " + car.Interior + '</p>' + '<p>' + "Mileage: "
                        + car.Mileage + '</p>' + '<p>' + "VIN: " + car.VIN + '</p></div>'

                        //right column
                        + '<div class="col-md-3"><p></p>' + '<p>' + "Sale Price: $" + car.SalePrice + '</p>' + '<p>' + "MSRP: $"
                        + car.MSRP + '</p>' + '<p><a href="EditVehicle/' + car.VehicleId + '" class="btn btn-primary" id="editbutton">Edit</a></p></div></div>'

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
    $('#results').hide();
    $('#searchoptions').hide();
    $('#adminsearch').hide();
    $('#cardetails').hide();
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