$(document).ready(function () {
    document.getElementById('boo').addEventListener('click', doit);
    $('#pir').click(doit);
    document.getElementById('cre').addEventListener('click', doit);
    $('#shi').click(doit);


    function doit(e) {
        e.preventDefault();//this will prevent the link element <a> reload the page
        //key word this will received the data element form the context/code, it knows which link has been clicked.
        var d = this.attributes['data-sent'].value;
        
        var tblStr = '<table class=table table-striped>';
        var tblr = '';
        if (d === 'Pirate') {
            $.ajax({
                url: '/Pirate/' + d,
                success: function (data) {
                    console.log('data from server: ' + data);
                    $.each(data, function (i, dataItem) {
                        tblr += '<tr><td>' + dataItem.a + '</td><td>' + new Date(parseInt((dataItem.b).replace('/Date(', ''))) + '</td></tr>'
                    });
                    tblStr += '<tr><td>Pirate</td><td>Assign Date</td></tr>' + tblr + '</table>';
                    $('#showdata').html(tblStr);
                },
                error: function () { alert('error when loading'); }
            });
        }

        if (d === 'Ship') {
            $.ajax({
                url: '/Pirate/' + d,
                success: function (data) {
                    console.log('data from server: ' + data);
                    $.each(data, function (i, dataItem) {
                        tblr += '<tr><td>' + dataItem.a + '</td><td>' + dataItem.b + '</td><td>' + dataItem.c + '</td></tr>'
                    });
                    tblStr += '<tr><td>Ship</td><td>Ton</td><td>Type</td></tr>' + tblr + '</table>';
                    $('#showdata').html(tblStr);
                },
                error: function () { alert('error when loading'); }
            });
        }

        if (d === 'Booty') {
            $.ajax({
                url: '/Pirate/' + d,
                success: function (data) {
                    console.log('data from server: ' + data);
                    $.each(data, function (i, dataItem) {
                        tblr += '<tr><td>' + dataItem.a + '</td><td>' + dataItem.b + '</td></tr>'
                    });
                    tblStr += '<tr><td>Pirate</td><td>Booty</td></tr>' + tblr + '</table>';
                    $('#showdata').html(tblStr);
                },
                error: function () { alert('error when loading'); }
            });
        }

        
        if (d === 'Crew') {
            $.ajax({
                url: '/Pirate/' + d,
                success: function (data) {
                    console.log('data from server: ' + data);
                    $.each(data, function (i, dataItem) {
                        tblr += '<tr><td>' + dataItem.a + '</td><td>' + dataItem.b + '</td><td>' + dataItem.c + '</td></tr>'
                    });
                    tblStr += '<tr><td>Id</td><td>Pirate\'s Crew</td><td>Crew\'s Booty</td></tr>' + tblr + '</table>';
                    $('#showdata').html(tblStr);
                },
                error: function () { alert('error when loading'); }
            });
        }
    }

   
    $('#search').click(function () {
        var pnam = $('#pirate').val();
        var u = '<ol>Pirate: ';
        var l =''; 
        console.log('input is: '+pnam);
        if (!pnam) { alert('please input pirate name that you like to search'); }
        $.ajax
        ({
            url: '/Pirate/Search/',
            data:{name:pnam},
            success: function (message) {
                console.log('success:' + message.namelist);
                if (!message.found) { $('#showp').html(message.info).addClass('text-danger'); return false}
                $.each(message.namelist, function (i, item) {
                    l+='<li>'+item+'</li>'
                });
                u+=l+'</ol>'
                $('#showp').html(u).addClass('text-danger');
            },
            error: function () { alert('error when loading');}
        });
    });


    $(document).click(function () { $('#showdata').html('');$('#showp').html(''); });
});