$(document).ready(function () {
    newcarsearch();
});

var url = "http://localhost:50822";

function fail(response) {
    console.log("Failed!", response);
}

function newcarsearch() {
    //$('#results').hide();
    $('#NewVehicleOptions').show();
    $('#newsearch').show();
    $('#cardetails').hide();

    $('#newsearch').on('click', function () {
        var minPrice = $('#minpricevalue').val();
        var maxPrice = $('#maxpricevalue').val();
        var minYear = $('#minyearvalue').val();
        var maxYear = $('#maxyearvalue').val();
        var search = $('#searchbar').val();
        //GetAll(search, minPrice, maxPrice, minYear, maxYear);

        //if (search == "" && minPrice == 0 && maxPrice <= 99999999 && minYear >= 1 && maxYear <= 9999) {

        //}

        if (search == "") {
            search="empty"
        }

            $.ajax({
                type: 'GET',
                url: url + "/Inventory/New/" + search + "/" + minPrice + "/" + maxPrice + "/" + minYear + "/" + maxYear,
                success: function (carDisplay) {
                    //alert('Ok');
                    $('#results').empty();
                    $('#results').show();
                    var resultsDiv = $('#results');
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
    $('#results').hide();
    $('#NewVehicleOptions').hide();
    $('#newsearch').hide();
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
                + car.MSRP + '</p></div></div>' + '<div class"row">' + "Description: " + car.Description + '</div>'

                //description div
                + '<div class="col-md-offset-11"><a href="ContactUs/' + car.VehicleId + '" class="btn btn-primary" id="NewContactUs">Contact Us</a></div>'

            carDetails.append(carInfo);
        },

        error: function () {
            alert('Details!');
        }

    });
}

//function GetAllNew(search, minPrice, maxPrice, minYear, maxYear) {
//    var isNew = true
//    $.get(url + "/Inventory" + "/" + search + "/" + minPrice + "/" + maxPrice + "/" + minYear + "/" + maxYear + "/" + isNew)
//        .done(function (response) {
//            vue.GetAllNew = response;
//        }).fail(fail);
//}

//function GetAllUsed(search, minPrice, maxPrice, minYear, maxYear) {
//    var isNew = false;
//    $.get(url + "/Inventory" + "/" + search + "/" + minPrice + "/" + maxPrice + "/" + minYear + "/" + maxYear + "/" + isNew)
//        .done(function (response) {
//            vue.GetAllNew = response;
//        }).fail(fail);
//}