const contactForm = document.getElementById("contactForm");
const successPopup = document.getElementById("successPopup");
const closePopupBtn = document.getElementById("closePopupBtn");

contactForm.addEventListener("submit", function (event) {
  event.preventDefault(); // Prevent the default form submission

  // --- Optional: Add form validation logic here if needed ---
  // If validation fails, you might show an error message and return

  // Simulate sending data (replace with actual AJAX call if needed)
  console.log("Form submitted (simulated)");

  // Show the success popup
  successPopup.classList.remove("hidden");
  successPopup.classList.add("flex"); // Use flex to enable centering

  // Reset the form fields
  contactForm.reset();

  // --- Optional: Hide labels that might float due to reset ---
  // Force labels back to placeholder state after reset if needed
  // This might be necessary if the :not(:placeholder-shown) state persists visually
  // Example for one field (repeat for others if needed):
  // const fullNameInput = document.getElementById('fullName');
  // fullNameInput.dispatchEvent(new Event('input', { bubbles: true })); // Trigger update
  // fullNameInput.dispatchEvent(new Event('blur', { bubbles: true })); // Trigger blur to reset label visual state
  // Or more simply, manually adjust label classes if the peer logic doesn't reset perfectly on form.reset()
});

// Add event listener to the close button
closePopupBtn.addEventListener("click", function () {
  successPopup.classList.add("hidden");
  successPopup.classList.remove("flex");
});

// Optional: Close popup if clicking outside the modal content
successPopup.addEventListener("click", function (event) {
  // Check if the click is on the background overlay itself
  if (event.target === successPopup) {
    successPopup.classList.add("hidden");
    successPopup.classList.remove("flex");
  }
});
