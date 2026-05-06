<%@ Page Title="Rating" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="DecusTest.UI.Web._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <style>
        label { display:block; margin-top:10px; }
        .result-box {
            border:1px solid #ccc;
            padding:15px;
            margin-top:20px;
            background:#f9f9f9;
        }
        .result-box h3 { margin-top:0; }
        .result-field { margin:10px 0; }
    </style>
    <script>
        function getInputData(raterId) {
			// Collect values
			var tiv = parseFloat(document.getElementById("tiv").value);
			var state = document.getElementById("state").value;
			var distance = parseFloat(document.getElementById("distance").value);

			// Basic validation
			if (isNaN(tiv) || tiv <= 0) {
				alert("Total Insured Value must be a number greater than 0");
				return;
			}
			if (isNaN(distance) || distance <= 0) {
				alert("Distance to water must be a number greater than 0");
				return;
			}

            var riskData = {                
				TotalInsuredValue: tiv,
				State: state,
				DistanceToWater: distance
            };

            var ratingSelections = [];
            if (raterId > 0) {
                // Collect only dropdowns for this rater                
                var selects = document.querySelectorAll(".rating-option[data-rater='" + raterId + "']");
                selects.forEach(function (sel) {
                    ratingSelections.push({
                        Name: sel.name,
                        SelectedValue: normalizeValue(sel.value)
                    });
                });
            }

            return {
                RaterId: raterId,
                RiskData: riskData,
                RatingOptions: ratingSelections
            }
        }

        function normalizeValue(val) {
            return val === "true" ? true :
                val === "false" ? false :
                    (!isNaN(val) && val.trim() !== "" ? parseFloat(val) : val);
        }

        function calculate(raterId) {
            var jsonData = getInputData(raterId);

            if (raterId === null) {
                // Clear old boxes
                document.querySelectorAll(".result-box").forEach(function (box) {
                    box.remove();
                });
            }

            if (jsonData !== null && jsonData !== undefined) {
                return sendDataToRater(jsonData);
            }
        }

		function sendDataToRater(jsonData) {

            fetch("/raters/get-rates.ashx", {
                method: "POST",
                headers: {
                    "Content-Type": "application/json"
                },
                body: JSON.stringify(jsonData)
            })
                .then(response => response.json())
                .then(data => renderResults(data))
                .catch(error => alert("Error: " + error));
        }

        function renderResults (response) {
            if (response.Success) {
                response.RatingValues.forEach(function (rater) {
                    renderResultBox(rater);
                });
            } else {
                alert("Errors: " + response.ErrorMessages.join(", "));
            }
        }


        function renderResultBox(rater) {
            var newBox = document.createElement("div");
            newBox.className = "result-box";
            newBox.id = "rater-" + rater.RaterId;

            var title = document.createElement("h3");
            title.textContent = rater.RaterName || "Result";
            newBox.appendChild(title);

            if (rater.RatingOptions && Array.isArray(rater.RatingOptions)) {
                rater.RatingOptions.forEach(function (opt) {
                    var label = document.createElement("label");
                    label.textContent = opt.NiceName;

                    var select = document.createElement("select");
                    select.name = opt.Name;
                    select.className = "rating-option";
                    select.dataset.rater = rater.RaterId;

                    if (opt.Options && Array.isArray(opt.Options)) {
                        opt.Options.forEach(function (o) {
                            var option = document.createElement("option");
                            option.value = o.Value;
                            option.textContent = o.Key;
                            if (o.Key == opt.SelectedValue || o.Value == opt.SelectedValue) {
                                option.selected = true;
                            }
                            select.appendChild(option);
                        });
                    } else {
                        var option = document.createElement("option");
                        option.value = opt.SelectedValue;
                        option.textContent = opt.SelectedValue;
                        option.selected = true;
                        select.appendChild(option);
                    }

                    select.addEventListener("change", function () {
                        calculate(rater.RaterId);
                    });

                    newBox.appendChild(label);
                    newBox.appendChild(select);
                });
            }

            var premiumDiv = document.createElement("div");
            premiumDiv.className = "result-field";
            premiumDiv.textContent = "Premium: " + rater.Premium;
            newBox.appendChild(premiumDiv);

            if (rater.SecondaryPremium !== null) {
                var secPremiumDiv = document.createElement("div");
                secPremiumDiv.className = "result-field";
                secPremiumDiv.textContent = "Secondary Premium: " + rater.SecondaryPremium;
                newBox.appendChild(secPremiumDiv);
            }

            // Replace existing box in place to preserve order
            var oldBox = document.getElementById("rater-" + rater.RaterId);
            if (oldBox) {
                oldBox.parentNode.replaceChild(newBox, oldBox);
            } else {
                document.body.appendChild(newBox);
            }
        }

    </script>

    <main>
        <div class="row">
			<h2>Risk Data</h2>

			<label for="tiv">Total Insured Value</label>
			<input type="number" id="tiv" min="1" step="any" >

			<label for="state">State</label>
			<select id="state">
				<option value="FL">Florida</option>
				<option value="TX">Texas</option>
			</select>

			<label for="distance">Distance to water (in miles)</label>
			<input type="number" id="distance" min="1" step="any" >

			<br><br>
			<button onclick="calculate();return false;">Calculate</button>

            <h2>Rates</h2>
        </div>
    </main>

</asp:Content>
