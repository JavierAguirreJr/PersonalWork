$(document).ready(function () {
    purchasedetail();
});

function purchasedetail() {
    $('#customerform').show();

    var url = "http://localhost:50822";
    var pagePath = window.location.pathname.split("/");
    var id = pagePath[3];
    $.ajax({
        type: 'GET',
        url: url + "/Inventory/Details/" + id,
        success: function (car) {

                var resultsDiv = $('#details');
                '<br />'
                var carInfo =
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
                + car.MSRP + '</p></div></div>'

                //description row
                 '<div class="col-md-12">' + car.Description + '</div>'

                //closing tag for entire div
                 '</div>'

                 resultsDiv.append(carInfo);

        },

        error: function () {
            alert('Details!');
        }

    });
}