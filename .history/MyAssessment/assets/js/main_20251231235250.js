// FAQ Toggle Functionality
document.addEventListener('DOMContentLoaded', function() {
    const faqItems = document.querySelectorAll('.faq-item');
    
    faqItems.forEach(item => {
        const question = item.querySelector('.faq-question');
        
        question.addEventListener('click', () => {
            const isActive = item.classList.contains('active');
            
            // Close all FAQ items
            faqItems.forEach(faq => faq.classList.remove('active'));
            
            // Open clicked item if it wasn't active
            if (!isActive) {
                item.classList.add('active');
            }
        });
    });
});

// Module Tab Functionality
document.addEventListener('DOMContentLoaded', function() {
    const tabBtns = document.querySelectorAll('.tab-btn');
    
    tabBtns.forEach(btn => {
        btn.addEventListener('click', () => {
            // Remove active class from all tabs
            tabBtns.forEach(tab => tab.classList.remove('active'));
            
            // Add active class to clicked tab
            btn.classList.add('active');
            
            // Here you can add logic to show/hide different module content
            // based on the selected tab
        });
    });
});

// Smooth Scrolling for Navigation Links
document.addEventListener('DOMContentLoaded', function() {
    const navLinks = document.querySelectorAll('.nav-link');
    
    navLinks.forEach(link => {
        link.addEventListener('click', function(e) {
            const href = this.getAttribute('href');
            
            if (href.startsWith('#')) {
                e.preventDefault();
                const target = document.querySelector(href);
                
                if (target) {
                    target.scrollIntoView({
                        behavior: 'smooth',
                        block: 'start'
                    });
                }
            }
        });
    });
});

// Search Functionality
document.addEventListener('DOMContentLoaded', function() {
    const searchInput = document.querySelector('.search-box input');
    const searchBtn = document.querySelector('.search-box button');
    
    if (searchBtn) {
        searchBtn.addEventListener('click', function() {
            const searchTerm = searchInput.value.trim();
            
            if (searchTerm) {
                // Implement search functionality here
                console.log('Searching for:', searchTerm);
                // You can redirect to a search results page or filter content
            }
        });
    }
    
    if (searchInput) {
        searchInput.addEventListener('keypress', function(e) {
            if (e.key === 'Enter') {
                searchBtn.click();
            }
        });
    }
});

// Mobile Menu Toggle (if needed)
function toggleMobileMenu() {
    const navMenu = document.querySelector('.nav-menu');
    navMenu.classList.toggle('active');
}

// Add loading animation for buttons
document.addEventListener('DOMContentLoaded', function() {
    const buttons = document.querySelectorAll('.btn-module, .btn-primary');
    
    buttons.forEach(btn => {
        btn.addEventListener('click', function() {
            // Add loading state
            const originalText = this.textContent;
            this.textContent = 'Loading...';
            this.disabled = true;
            
            // Simulate loading (remove this in production)
            setTimeout(() => {
                this.textContent = originalText;
                this.disabled = false;
            }, 1000);
        });
    });
});