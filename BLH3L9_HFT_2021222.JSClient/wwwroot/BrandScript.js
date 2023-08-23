let brands = [];
let connection = null;
let brandIdToUpdate = -1;
getdata();
setupSignalR();

function setupSignalR() {
    connection = new signalR.HubConnectionBuilder()
        .withUrl("http://localhost:61344/hub")
        .configureLogging(signalR.LogLevel.Information)
        .build();

    connection.on("BrandDeleted", (user, message) => {
        getdata();
    });

    connection.on("BrandCreated", (user, message) => {
        getdata();
    });

    connection.on("BrandUpdated", (user, message) => {
        getdata();
    });

    connection.onclose(async () => {
        await start();
    });

    start();
}

async function start() {
    try {
        await connection.start();
        console.log("SignalR connected.");
    } catch (err) {
        console.log(err);
        setTimeout(start, 5000);
    }
}

async function getdata() {
    await fetch('http://localhost:61344/brand')
        .then(x => x.json())
        .then(y => {
            brands = y;
            console.log(brands)
            display();
        });
}

function display() {
    document.getElementById('results').innerHTML = "";
    brands.forEach(z => {
        document.getElementById('results').innerHTML += "<tr><td>" + z.brandId
            + "</td><td>" + z.brandName + "</td><td>" + z.percentage + "</td><td>" +
            `<button type="button" onclick="remove(${z.brandId})">Delete</button>` +
            `<button type="button" onclick="showupdate(${z.brandId})">Update</button>`
            + "</td></tr>";
        console.log(z.brandName);
    });
}

function create() {
    let brandname = document.getElementById('brandname').value;
    let percentage = document.getElementById('percentage').value;
    fetch('http://localhost:61344/brand', {
        method: 'POST',
        headers: { 'Content-Type': 'application/json', },
        body: JSON.stringify(
            { brandName: brandname, percentage: percentage }),
    })
        .then(response => response)
        .then(data => {
            console.log('Success:', data);
            getdata();
        })
        .catch((error) => { console.log('Error:', error); });
}

function showupdate(id) {
    document.getElementById('brandnametoupdate').value = brands.find(t => t['brandId'] == id)['brandName'];
    document.getElementById('percentagetoupdate').value = brands.find(t => t['brandId'] == id)['percentage'];
    document.getElementById('updateform').style.display = 'inline';
    brandIdToUpdate = id;
}

function update() {
    document.getElementById('updateform').style.display = 'none';
    let brandname = document.getElementById('brandnametoupdate').value;
    let percentagea = document.getElementById('percentagetoupdate').value;
     fetch('http://localhost:61344/brand', {
        method: 'PUT',
        headers: { 'Content-Type': 'application/json', },
         body: JSON.stringify(
             { brandName: brandname, brandId: brandIdToUpdate, percentage: percentagea }),
    })
        .then(response => response)
        .then(data => {
            console.log('Success:', data);
            getdata();
        })
        .catch((error) => { console.log('Error:', error); });
}

function remove(id) {
    fetch('http://localhost:61344/brand/' + id, {
        method: 'DELETE',
        headers: { 'Content-Type': 'application/json', },
        body: null
    })
        .then(response => response)
        .then(data => {
            console.log('Success:', data);
            getdata();
        })
        .catch((error) => { console.error('Error:', error); });
}