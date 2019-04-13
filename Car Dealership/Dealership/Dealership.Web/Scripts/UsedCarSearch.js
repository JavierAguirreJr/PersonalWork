$(document).ready(function () {
    usedcarsearch();
});

var url = "http://localhost:50822";

function fail(response) {
    console.log("Failed!", response);
}

function usedcarsearch() {
    //$('#usedresults').hide();
    $('#UsedSearchOptions').show();
    $('#UsedSearch').show();
    $('#cardetails').hide();
    
    $('#UsedSearch').on('click', function () {
        var minPrice = $('#minpricevalue').val();
        var maxPrice = $('#maxpricevalue').val();
        var minYear = $('#minyearvalue').val();
        var maxYear = $('#maxyearvalue').val();
        var search = $('#searchbar').val();

        if (search == "") {
            search = "empty"
        }

        $.ajax({
            type: 'GET',
            url: url + "/Inventory/Used/" + search + "/" + minPrice + "/" + maxPrice + "/" + minYear + "/" + maxYear,
            success: function (carDisplay) {
                //alert('Ok');
                $('#usedresults').empty();
                $('#usedresults').show();
                var resultsDiv = $('#usedresults');
                '<br>'
                $.each(carDisplay, function (index, car) {

                    var carInfo =
                        //Divided code into parts, I hate when it runs to the sides :P
                        //left column
                        '<div class="row"><div class="col-md-12"><div class="col-md-3"><p>' + car.Year + " " + " " + car.ModelName.MakeName.Make
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
                        + car.MSRP + '</p>' + '<p><button class="btn btn-primary" id="CarDetailsButton"  + onClick="VehicleDetails(' + car.VehicleId + ')">Details</button></p></div>'

                        //closing tag for entire div
                        '</div></div>'

                        resultsDiv.append(carInfo);
                })
            },

            error: function () {
                alert('FAILURE!');
            }

        });
    });
}

function VehicleDetails(id) {
    //$('#CarDetailsButton').on('click', function () {
        //var carId = car.VehicleId;
    //var id = $('#CarDetailsButton').val();
    $('#usedresults').hide();
    $('#UsedSearchOptions').hide();
    $('#UsedSearch').hide();
    $('#cardetails').show();
        $.ajax({
            type: 'GET',
            url: url + "/Inventory/Details/" + id,
            success: function (car) {

                //if (car.IsSold=true) {
                //    var CarSold = '<input type="button" class="btn btn-danger" value="Sold" />';
                //}
                //else {
                //    var ContactForCar = //find way to link to contact page
                //}

                var carDetails = $('#cardetails');

                    var carInfo =
                        //Divided code into parts, I hate when it runs to the sides :P
                        //left column
                        '<div class="col-md-12"><div class="row"><div class="col-md-3"><p>'+ car.Year + " " + car.ModelName.MakeName.Make
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
                        + car.MSRP + '</p></div></div>' + '<div class"row">' + "Description: " + car.Description +'</div>'

                        //description div
                        + '<div class="col-md-offset-11"><a href="ContactUs/' + car.VehicleId + '" class="btn btn-primary" id="UsedContactUs">Contact Us</a></div>'

                        //closing tag for entire div
                        //'</div>'

                        carDetails.append(carInfo);
            },

            error: function () {
                alert('Details!');
            }

        });
    //});
}