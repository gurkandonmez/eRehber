$.getJSON("../il-bolge.json", function (sonuc) {
    var row = "";
    $.each(sonuc, function (index, value) {
        row += '<option value="' + value.il + '">' + value.il + '</option>';
    })
    $("#il").append(row);
});


$(document).on("click", "#btnAra", function () {


    var IlAdi = $("#inputil").val();
    var SubeAdi = sube.options[sube.selectedIndex].text;
    $('#content').html('');
   // $('#inputil').val('');
    //      var formValues = $("#ListeForm").serialize();

    $.ajax({
        url: '/Home/Index/',
        type: 'POST',
        dataType: 'json',
        data: { 'IlAdi': IlAdi, 'SubeAdi': SubeAdi },
        success: function (data) {


            for (var item in data.Result) {


                var deger = '<tr>   <td>' + data.Result[item].SubeAdi + '</td><td>' + data.Result[item].Ad + " " + data.Result[item].Soyad + '</td> <td>' + data.Result[item].Gorev + '<td>' + data.Result[item].Rutbe + '</td> <td>' + data.Result[item].DahiliNo + '</td></tr>';

                $('#content').append(deger);

            }

        },
        error: function (data) {
            alert("Bir hata olustu");
        },
        complete: function (data) {


            //alert('')

        }
    })


})