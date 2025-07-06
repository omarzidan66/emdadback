const placeholder = document.getElementById("lazy-counter-placeholder");

const observer = new IntersectionObserver(
  (entries, observer) => {
    entries.forEach((entry) => {
      if (entry.isIntersecting) {
        // Inject SVG into the DOM
        placeholder.innerHTML = `
            <div class="flex flex-col justify-center items-center w-full h-full">
              <div class="container">
                <svg class="counter" viewBox="0 0 100 100">
                  <circle cx="50" cy="50" r="45" class="background-circle" />
                  <circle cx="50" cy="50" r="45" class="progress-circle" transform="rotate(-90 50 50)" style="stroke: #008000; fill: none; stroke-width: 10;" />
                  <text x="50" y="55" text-anchor="middle" class="counter-text" fill="#fff" font-size="14">0%</text>
                </svg>
                
              </div>
                      <p class="text-black mt-5">نسبة البلاغات التي تمت معالجتها</p>

            </div>
          `;

        // After SVG is injected, start the counter animation
        let count = 0;
        const maxCount = 75;
        const textElement = placeholder.querySelector(".counter-text");
        const progressCircle = placeholder.querySelector(".progress-circle");
        const circumference = 2 * Math.PI * 45;
        progressCircle.style.strokeDasharray = circumference;
        progressCircle.style.strokeDashoffset = circumference;

        function updateCounter() {
          if (count <= maxCount) {
            textElement.textContent = count + "%";
            let progress = (count / 100) * circumference;
            progressCircle.style.strokeDashoffset = circumference - progress;
            count++;
            setTimeout(updateCounter, 30);
          }
        }

        updateCounter();

        observer.unobserve(placeholder);
      }
    });
  },
  {
    root: null,
    threshold: 0.1,
  }
);

observer.observe(placeholder);
