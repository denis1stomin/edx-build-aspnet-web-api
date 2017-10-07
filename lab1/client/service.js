const request = require('request');

var url = 'http://localhost:5000/api/products/1';
request(url, (error, response, body) => {
    if (!error && response.statusCode === 200) {
        const resp = JSON.parse(body);
        console.log("Got a response: ");
        for(var propName in resp) {
            console.log(propName, ' = ', resp[propName]);
        }
    } else {
        console.log("Got an error: ", error, ", status code: ", response.statusCode);
    }
});
