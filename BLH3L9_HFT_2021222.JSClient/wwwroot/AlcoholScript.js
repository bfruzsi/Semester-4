let alcohols = [];
let connection = null;
let alcoholIdToUpdate = -1;
getdata();
setupSignalR();

function setupSignalR() {
    connection = new signalR.HubConnectionBuilder()
        .withUrl("http://localhost:61344/hub")
        .configureLogging(signalR.LogLevel.Information)
        .build();

    connection.on("AlcoholDeleted", (user, message) => {
        getdata();
    });

    connection.on("AlcoholCreated", (user, message) => {
        getdata();
    });

    connection.on("AlcoholUpdated", (user, message) => {
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
    await fetch('http://localhost:61344/alcohol')
        .then(x => x.json())
        .then(y => {
            alcohols = y;
            console.log(alcohols)
            display();
        });
}

function display() {
    document.getElementById('results').innerHTML = "";
    alcohols.forEach(z => {
        document.getElementById('results').innerHTML += "<tr><td>" + z.alcoholId
            + "</td><td>" + z.alcoholName + "</td><td>" + z.grain + "</td><td>" +
            `<button type="button" onclick="remove(${z.alcoholId})">Delete</button>` +
            `<button type="button" onclick="showupdate(${z.alcoholId})">Update</button>`
            + "</td></tr>";
        console.log(z.alcoholName);
    });
}

function create() {
    let alcoholname = document.getElementById('alcoholname').value;
    let grain = document.getElementById('grain').value;
    fetch('http://localhost:61344/alcohol', {
        method: 'POST',
        headers: { 'Content-Type': 'application/json', },
        body: JSON.stringify(
            { alcoholName: alcoholname, grain: grain }),
    })
        .then(response => response)
        .then(data => {
            console.log('Success:', data);
            getdata();
        })
        .catch((error) => { console.log('Error:', error); });
}

function showupdate(id) {
    document.getElementById('alcoholnametoupdate').value = alcohols.find(t => t['alcoholId'] == id)['alcoholName'];
    document.getElementById('grainnametoupdate').value = alcohols.find(t => t['alcoholId'] == id)['grain'];
    document.getElementById('updateform').style.display = 'inline';
    alcoholIdToUpdate = id;
}

function update() {
    document.getElementById('updateform').style.display = 'none';
    let alcoholname = document.getElementById('alcoholnametoupdate').value;
    let grain = document.getElementById('grainnametoupdate').value;
    fetch('http://localhost:61344/alcohol', {
        method: 'PUT',
        headers: { 'Content-Type': 'application/json', },
        body: JSON.stringify(
            { alcoholName: alcoholname, alcoholId: alcoholIdToUpdate, grain: grain }),
    })
        .then(response => response)
        .then(data => {
            console.log('Success:', data);
            getdata();
        })
        .catch((error) => { console.log('Error:', error); });
}

function remove(id) {
    fetch('http://localhost:61344/alcohol/' + id, {
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