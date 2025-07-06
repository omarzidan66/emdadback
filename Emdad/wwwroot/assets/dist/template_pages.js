class SpecialHeader extends HTMLElement {
  connectedCallback() {
    this.innerHTML = `
      <header class="fixed w-full top-0 left-0 z-50">
      <nav
        class="navbar navbar-light flex justify-between items-center relative bottom-10">
        <div
          class="center_box w-1/2 flex relative justify-end items-center mb-5">
          <button
            class="navbar-toggler collapsed text-black font-bold flex items-center pt-10"
            aria-expanded="false">
            <span class="navbar-toggler-icon-new"></span>
          </button>
        </div>
        <div class="right_box w-1/2 flex justify-end items-center">
          <a href="">
            <img
              class="w-40 h-auto"
              src="/assets/Images/EmdadLogo.png"
              alt="" />
          </a>
        </div>
        <div
          class="navbar-collapse collapse w-full h-[100vh] absolute top-0 left-0">
          <div class="navbar-inner h-full relative flex mt-16">
            <ul
              class="navbar-nav w-full sm:flex justify-center items-center sm:flex-row sm:flex-wrap my-[20vh] sm:mx-56 font-bold">
              <li
                class="nav-item sm:w-1/2 w-full min-h-[9vh] flex justify-center items-center sm:text-2xl text-lg sm:mb-5 delay-100 text-white">
                <a href="./home_page.html#banner">الصفحة الرئيسية</a>
              </li>
              <li
                class="nav-item sm:w-1/2 w-full min-h-[9vh] flex justify-center items-center sm:text-2xl text-lg sm:mb-5 delay-100 text-white">
                <a href="./home_page.html#services">دليل الخدمات</a>
              </li>
              <li
                class="nav-item sm:w-1/2 w-full min-h-[9vh] flex justify-center items-center sm:text-2xl text-lg sm:mb-5 delay-150 text-white">
                <a href="./home_page.html#reportss">بلاغات عامة</a>
              </li>
              <li
                class="nav-item sm:w-1/2 w-full min-h-[9vh] flex justify-center items-center sm:text-2xl text-lg sm:mb-5 delay-150 text-white">
                <a href="./home_page.html#tenders">العطاءات</a>
              </li>
              <li
                class="nav-item sm:w-1/2 w-full min-h-[9vh] flex justify-center items-center sm:text-2xl text-lg sm:mb-5 delay-200 text-white">
                <a href="./home_page.html#achievements">انجازاتنا بالارقام</a>
              </li>
              <li
                class="nav-item sm:w-1/2 w-full min-h-[9vh] flex justify-center items-center sm:text-2xl text-lg sm:mb-5 delay-200 text-white">
                <a href="./home_page.html#feedbacks">التعليقات و الاقتراحات</a>
              </li>
              <li
                class="nav-item sm:w-1/2 w-full min-h-[9vh] flex justify-center items-center sm:text-2xl text-lg sm:mb-5 delay-200 text-white">
                <a href="./home_page.html#myReports"> بلاغاتي</a>
              </li>
              <li
                class="nav-item sm:w-1/2 w-full min-h-[9vh] flex justify-center items-center sm:text-2xl text-lg sm:mb-5 delay-300 text-white">
                <a href="./CitizensSettings/Index"> الاعدادات </a>
              </li>
            </ul>
          </div>
        </div>
      </nav>
    </header>
      `;

    // Initialize navigation behavior after the component is rendered
    this.initNavigation();
  }

  initNavigation() {
    const navButton = document.querySelector(".navbar-toggler");
    const menuPanel = document.querySelector(".navbar-collapse");
    const navLinks = document.querySelectorAll(".navbar-nav a");

    // Toggle menu when navbar toggler is clicked
    navButton.addEventListener("click", function () {
      menuPanel.classList.toggle("show");
      menuPanel.classList.toggle("collapse");

      const isExpanded = menuPanel.classList.contains("show");
      navButton.setAttribute("aria-expanded", isExpanded);
      document.body.style.overflow = isExpanded ? "hidden" : "auto";
    });

    navLinks.forEach(function (link) {
      link.addEventListener("click", function (e) {
        e.preventDefault();
        const href = this.getAttribute("href");

        // Close menu
        menuPanel.classList.remove("show");
        menuPanel.classList.add("collapse");
        navButton.setAttribute("aria-expanded", "false");

        setTimeout(function () {
          document.body.style.overflow = "auto";

          // Remove 'active' class and inline color from all links
          navLinks.forEach((nav) => {
            nav.classList.remove("active");
            nav.style.removeProperty("color");
          });

          // Set active class and inline color with !important
          link.classList.add("active");
          link.style.setProperty("color", "#CEB579", "important");

          // Handle navigation
          if (href && href !== "") {
            if (href.startsWith("#")) {
              const targetElement = document.querySelector(href);
              if (targetElement) {
                targetElement.scrollIntoView({ behavior: "smooth" });
              }
            } else {
              window.location.href = href;
            }
          }
        }, 300);
      });
    });

    // Set the home link as active initially (and apply color)
    // const homeLink = document.querySelector('.navbar-nav a[href="#banner"]');
    // if (homeLink) {
    //   homeLink.classList.add("active");
    //   homeLink.style.setProperty("color", "#CEB579", "important");
    // }
  }
}

// Create the Footer custom element
class SpecialFooter extends HTMLElement {
  connectedCallback() {
    this.innerHTML = `
       <footer class="h-fit bg-neutral-900 text-white">
    <div class="p-10 grid sm:grid-cols-3 grid-cols-1 gap-3">
      <div class="grid grid-cols-1 mb-5">
        <p class="font-bold text-[#CEB579] text-2xl mb-3">إمداد</p>
        <p class="mb-5 text-white/70">
          Lorem ipsum dolor sit amet consectetur adipisicing elit. Eligendi enim magni nisi id in consectetur sint est beatae repellendus eius voluptate aspernatur maiores quidem illum amet ut, quis aliquid explicabo.
        </p>
        <div class="grid grid-cols-4">
          <i
            class="fa fa-instagram w-10 h-10 rounded-full bg-white/5 flex items-center justify-center transition-all duration-300 ease-in-out text-[#e0e0e0] no-underline text-sm border border-white/10 hover:bg-[#d4af37] hover:text-white hover:-translate-y-[5px] hover:shadow-[0_5px_15px_rgba(212,175,55,0.4)]"
            aria-hidden="true"></i>
          <i
            class="fa fa-facebook w-10 h-10 rounded-full bg-white/5 flex items-center justify-center transition-all duration-300 ease-in-out text-[#e0e0e0] no-underline text-sm border border-white/10 hover:bg-[#d4af37] hover:text-white hover:-translate-y-[5px] hover:shadow-[0_5px_15px_rgba(212,175,55,0.4)]"
            aria-hidden="true"></i>
          <i
            class="fa fa-twitter w-10 h-10 rounded-full bg-white/5 flex items-center justify-center transition-all duration-300 ease-in-out text-[#e0e0e0] no-underline text-sm border border-white/10 hover:bg-[#d4af37] hover:text-white hover:-translate-y-[5px] hover:shadow-[0_5px_15px_rgba(212,175,55,0.4)]"
            aria-hidden="true"></i>
          <i
            class="fa fa-linkedin w-10 h-10 rounded-full bg-white/5 flex items-center justify-center transition-all duration-300 ease-in-out text-[#e0e0e0] no-underline text-sm border border-white/10 hover:bg-[#d4af37] hover:text-white hover:-translate-y-[5px] hover:shadow-[0_5px_15px_rgba(212,175,55,0.4)]"
            aria-hidden="true"></i>
        </div>
      </div>
      <div class="flex flex-col items-start sm:items-center mb-5">
        <div class="">
          <p class="text-lg font-medium pb-2">روابط سريعة</p>
          <div class="w-10 h-[3px] rounded-md bg-[#CEB579] mb-3"></div>
        </div>
        <div class="grid grid-cols-1">
          <p class="mb-3  text-white/70 no-underline text-sm transition-all duration-300 ease-in-out inline-block relative pl-[15px] hover:text-[#d4af37] hover:translate-x-[5px] before:content-['›'] before:absolute before:left-0 before:text-[#d4af37] before:transition-all before:duration-300 before:ease-in-out hover:before:translate-x-[3px]">
            
            <a href="#reportss">بلاغات عامة</a>
          </p >
          <p class="mb-3  text-white/70 no-underline text-sm transition-all duration-300 ease-in-out inline-block relative pl-[15px] hover:text-[#d4af37] hover:translate-x-[5px] before:content-['›'] before:absolute before:left-0 before:text-[#d4af37] before:transition-all before:duration-300 before:ease-in-out hover:before:translate-x-[3px]">
           
            <a href="#services">دليل الخدمات</a>
          </p >
          <p class="mb-3  text-white/70 no-underline text-sm transition-all duration-300 ease-in-out inline-block relative pl-[15px] hover:text-[#d4af37] hover:translate-x-[5px] before:content-['›'] before:absolute before:left-0 before:text-[#d4af37] before:transition-all before:duration-300 before:ease-in-out hover:before:translate-x-[3px]">
            
            <a href="#achievements">انجازاتنا</a>
          </p >
          <p class="mb-3  text-white/70 no-underline text-sm transition-all duration-300 ease-in-out inline-block relative pl-[15px] hover:text-[#d4af37] hover:translate-x-[5px] before:content-['›'] before:absolute before:left-0 before:text-[#d4af37] before:transition-all before:duration-300 before:ease-in-out hover:before:translate-x-[3px]">
            
            <a href="">تواصل معنا</a>
          </p>
        </div>
      </div>
      <div class="grid grid-cols-1 mb-5">
        <div class="">
          <p class="text-lg font-medium pb-2"> اتصل بنا</p>
          <div class="w-10 h-[3px] rounded-md bg-[#CEB579] mb-3"></div>
        </div>
        <div class="text-white/70">
          <p class="mb-3">
            <i class="fa fa-map-marker mr-3" aria-hidden="true"></i>

            Khalda 
              Amman, 11953 Jordan
          </p>

          <p class="mb-3">
            <i class="fa fa-mobile mr-3" aria-hidden="true"></i> +962 79 999
            9999
          </p>
          <p class="mb-3">
            <i class="fa fa-envelope-o mr-3" aria-hidden="true"></i>
            emdad@gmail.com
          </p>
        </div>
        <div class="">
          <p class="text-lg font-medium pb-2">اخر التحديثات</p>
          <div class="w-10 h-[3px] rounded-md bg-[#CEB579] mb-3"></div>
        </div>
        <div class="relative">
          <input
            type="email"
            placeholder="Enter your email"
            class="w-full py-[14px] px-5 border-none bg-white/5 rounded-full text-white text-sm shadow-[inset_0_0_0_1px_rgba(255,255,255,0.1)] transition-all duration-300 ease-in-out focus:outline-none focus:shadow-[inset_0_0_0_1px_#d4af37,_0_0_0_3px_rgba(212,175,55,0.2)] focus:bg-white/[.08] placeholder-white/40" />
          <button
            type="submit"
            class="absolute right-[5px] top-[5px] h-[calc(100%-10px)] bg-[#CEB579] border-none px-5 text-white rounded-[25px] cursor-pointer transition-all duration-300 ease-in-out font-semibold hover:bg-[#b8860b] hover:shadow-[0_5px_15px_rgba(212,175,55,0.4)]">
            Subscribe
          </button>
        </div>
      </div>
    </div>
    <div class="w-full h-[2px] bg-[#CEB579]"></div>
    <div
      class="sm:grid sm:grid-cols-12 grid-cols-1 pt-5 p-10 w-full flex flex-col justify-between items-center">
      <p class="col-span-7 text-white/70 mb-2">
        2025 EMDAD. All rights reserved.
      </p>
      <div class="col-span-5 grid sm:grid-cols-3 grid-cols-1 text-white/70">
        <p class="mb-2">Privacy Policy</p>
        <p class="mb-2">Termp of service</p>
        <p class="mb-2">Cookie Policy</p>
      </div>
    </div>
  </footer>
    `;
  }
}

// Register both components
customElements.define("special-header", SpecialHeader);
customElements.define("special-footer", SpecialFooter);

// Handle scroll event
document.addEventListener("DOMContentLoaded", function () {
  const body = document.querySelector("body");

  window.addEventListener("scroll", function () {
    if (window.scrollY > 0) {
      body.classList.add("scrolled");
    } else {
      body.classList.remove("scrolled");
    }
  });
});
