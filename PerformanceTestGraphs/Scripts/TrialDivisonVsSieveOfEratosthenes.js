/// <reference path="jquery-1.7.1-vsdoc.js" />

var trialDivisionValues = [];
var sieveOfEraValues = [];
var clicksYet = false;
var searchRange = 0;
var tdRangeScanned = 0;
var soeRangeScanned = 0;
var somethingBadHappened = false;

$(function () {
    bindEvents();
    testPerformance();
});

function testPerformance() {

    somethingBadHappened = false;

    var maxLimit = parseInt(getLimitTextBox().val());    

    if (isNaN(maxLimit) || maxLimit < 0) {
        alert('Please enter a valid limit (whole number)');
        return false;
    }

    var minLimit = (maxLimit <= 500) ? 5 : 100;
    var increment = minLimit;

    trialDivisionValues = [];
    sieveOfEraValues = [];

    startWheel(maxLimit);

    for (var i = minLimit; i <= maxLimit; i += increment) {
        var constraint = { Limit: i, Increment: 0 };
        getPerformanceFromServer(constraint);        
    }

    var overflow = maxLimit % increment;
    if (overflow > 0) {
        var constraint = { Limit: maxLimit, Increment: 0 };
        getPerformanceFromServer(constraint);
    }

    return false;
}
      
function getPerformanceFromServer(constraint) {
    PrimeTime.Services.TrialDivision.GetPerformance(constraint, trialDivisionSuccess, onFail);
    PrimeTime.Services.SieveOfEratosthenes.GetPerformance(constraint, sieveOfEratosthenesSuccess, onFail);
}

function trialDivisionSuccess(result) {
    tdRangeScanned = result.RangeLimit;
    checkIfComplete(result);

    trialDivisionValues.push([result.NoPrimesFound, result.TimeTaken]);
    updateMessage(getTrialDivisionTimeTakenLabel(), getTrialDivisonLastPrimeFoundLabel(), result);
    plotComparison();
}

function sieveOfEratosthenesSuccess(result) {
    soeRangeScanned = result.RangeLimit;
    checkIfComplete(result);

    sieveOfEraValues.push([result.NoPrimesFound, result.TimeTaken]);
    updateMessage(getSieveTimeTakenLabel(), getSieveLastPrimeFoundLabel(), result);
    plotComparison();
}

function checkIfComplete(result) {
    if ((tdRangeScanned === searchRange) && (soeRangeScanned === searchRange)) {
        resetWheel();
    }
}

function updateMessage(timeLabel, lastPrimeLabel, result) {
    timeLabel.text(result.TimeTaken);
    lastPrimeLabel.text(result.LastPrimeFound);
}

function plotComparison() {
    var data = [
            {
                color: "rgba(255, 99, 71, 0.8)",       
                data: trialDivisionValues,
                label: "Trial Division",
                points: { show: true },
                lines: { show: true }
            },
            {
                color: "rgba(30, 144, 255, 0.8)",
                data: sieveOfEraValues,
                label: "Sieve of Eratosthenes",
                points: { show: true },
                lines: { show: true }                
            }
        ];

    $.plot(getComparisonChart(), data, {
        grid: {
            hoverable: true,
            clickable: true,
            minBorderMargin: 30
        },
        legend: { position: "nw" }
    });
}

function bindEvents() {
    getComparisonChart().bind("plothover", onPointHover);

    getTestButton().click(testPerformance);

    getTestButton().button({
        text: false,
        label: "test",
        icons: { primary: "ui-icon-play" }
    });

    getLimitTextBox().autocomplete({
        source: ["1000",
                 "2500",
                 "5000",
                 "10000",
                 "50000",
                 "100000"]
    });
}

function showTooltip(x, y, contents) {
    if (clicksYet)
        contents += (pointClicked) ? ' hello' : ' bye';

    $('<div id="tooltip">' + contents + '</div>').css({
        position: 'absolute',
        display: 'none',
        top: y + 5,
        left: x + 5,
        border: '1px solid #fdd',
        padding: '2px',
        'background-color': "#FFFFE0",
        opacity: 0.80
    }).appendTo("body").fadeIn(200);
}

function onPointHover(event, pos, item) {
    $("#x").text(pos.x.toFixed(0));
    $("#y").text(pos.y.toFixed(0));

    if (item) {
        if (previousPoint != item.datapoint) {
            previousPoint = item.datapoint;

            $("#tooltip").remove();
            var x = item.datapoint[0].toFixed(0),
                y = item.datapoint[1].toFixed(0);

            showTooltip(item.pageX, item.pageY,
                        x + " primes found in " + y + " ms");
        }
    }
    else {
        $("#tooltip").remove();
        clicksYet = false;
        previousPoint = null;
    }
}

function onFail(xhr, status, error) {
    if (somethingBadHappened !== true)
        alert("Something bad happened, please try again!");
    
    somethingBadHappened = true;    
    resetWheel();    
}

function startWheel(maxLimit) {
    searchRange = maxLimit;
    tdRangeScanned = 0;
    soeRangeScanned = 0;

    getTestButton().hide()
    getLimitTextBox().prop("disabled", true);
    getWaitWheelImage().show();
}

function resetWheel() {
    getTestButton().show();
    getLimitTextBox().prop("disabled", false);
    getWaitWheelImage().hide();
}

function getTrialDivisionTimeTakenLabel() {
    return $("#tdTimeTaken");
}

function getSieveTimeTakenLabel() {
    return $("#soeTimeTaken");
}

function getTrialDivisonLastPrimeFoundLabel() {
    return $("#tdLastPrimeFound");
}

function getSieveLastPrimeFoundLabel() {
    return $("#soeLastPrimeFound");
}

function getLimitTextBox() {
    return $("#limit");
}

function getTestButton() {
    return $("#test");
}

function getWaitWheelImage() {
    return $("#waitWheel");
}

function getComparisonChart() {
    return $("#comparisonChart");
}