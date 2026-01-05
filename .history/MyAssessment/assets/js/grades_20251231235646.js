// Grade calculation functions for dynamic input

function calculateKI3Average(element) {
    const studentId = element.id.split('_')[1];
    const uhInputs = document.querySelectorAll(`input[id^="txtUH_${studentId}_"]`);
    
    let total = 0;
    let count = 0;
    
    uhInputs.forEach(input => {
        const value = parseFloat(input.value);
        if (!isNaN(value) && value > 0) {
            total += value;
            count++;
        }
    });
    
    const average = count > 0 ? (total / count).toFixed(1) : 0;
    const avgLabel = document.getElementById(`lblAvgUH_${studentId}`);
    if (avgLabel) {
        avgLabel.textContent = average;
    }
    
    calculateKI3Final(element);
}

function calculateKI3Final(element) {
    const studentId = element.id.split('_')[1];
    
    // Get average UH
    const avgUHLabel = document.getElementById(`lblAvgUH_${studentId}`);
    const avgUH = parseFloat(avgUHLabel ? avgUHLabel.textContent : 0);
    
    // Get PTS and PAS
    const ptsInput = document.getElementById(`txtPTS_${studentId}`);
    const pasInput = document.getElementById(`txtPAS_${studentId}`);
    
    const pts = parseFloat(ptsInput ? ptsInput.value : 0) || 0;
    const pas = parseFloat(pasInput ? pasInput.value : 0) || 0;
    
    // Calculate final score (UH 40%, PTS 30%, PAS 30%)
    const finalScore = (avgUH * 0.4 + pts * 0.3 + pas * 0.3).toFixed(1);
    
    // Update final score
    const finalLabel = document.getElementById(`lblFinalKI3_${studentId}`);
    if (finalLabel) {
        finalLabel.textContent = finalScore;
    }
    
    // Update predicate
    updatePredicate(studentId, finalScore, 'KI3');
}

function calculateKI4Final(element) {
    const studentId = element.id.split('_')[1];
    
    // Get all praktik scores
    const praktikInputs = document.querySelectorAll(`input[id^="txtPraktik_${studentId}_"]`);
    let praktikTotal = 0;
    let praktikCount = 0;
    
    praktikInputs.forEach(input => {
        const value = parseFloat(input.value);
        if (!isNaN(value) && value > 0) {
            praktikTotal += value;
            praktikCount++;
        }
    });
    
    const avgPraktik = praktikCount > 0 ? praktikTotal / praktikCount : 0;
    
    // Get Proyek and Portofolio
    const proyekInput = document.getElementById(`txtProyek_${studentId}`);
    const portofolioInput = document.getElementById(`txtPortofolio_${studentId}`);
    
    const proyek = parseFloat(proyekInput ? proyekInput.value : 0) || 0;
    const portofolio = parseFloat(portofolioInput ? portofolioInput.value : 0) || 0;
    
    // Calculate final score (Praktik 50%, Proyek 25%, Portofolio 25%)
    const finalScore = (avgPraktik * 0.5 + proyek * 0.25 + portofolio * 0.25).toFixed(1);
    
    // Update final score
    const finalLabel = document.getElementById(`lblFinalKI4_${studentId}`);
    if (finalLabel) {
        finalLabel.textContent = finalScore;
    }
    
    // Update predicate
    updatePredicate(studentId, finalScore, 'KI4');
}

function updatePredicate(studentId, score, type) {
    const kkm = 75; // Default KKM, should be dynamic based on competency
    let predicate = '';
    let predicateClass = '';
    
    if (score >= 90) {
        predicate = 'A';
        predicateClass = 'A';
    } else if (score >= 80) {
        predicate = 'B';
        predicateClass = 'B';
    } else if (score >= kkm) {
        predicate = 'C';
        predicateClass = 'C';
    } else if (score > 0) {
        predicate = 'D';
        predicateClass = 'D';
    } else {
        predicate = '-';
        predicateClass = '';
    }
    
    const predicateLabel = document.getElementById(`lblPredicate${type}_${studentId}`);
    if (predicateLabel) {
        predicateLabel.textContent = predicate;
        predicateLabel.className = `predicate ${predicateClass}`;
    }
}

// Tab switching functionality
document.addEventListener('DOMContentLoaded', function() {
    const tabButtons = document.querySelectorAll('.assessment-tabs .tab-btn');
    
    tabButtons.forEach(button => {
        button.addEventListener('click', function(e) {
            // Remove active class from all tabs in the same container
            const container = this.closest('.grade-input-container');
            const tabs = container.querySelectorAll('.tab-btn');
            tabs.forEach(tab => tab.classList.remove('active'));
            
            // Add active class to clicked tab
            this.classList.add('active');
        });
    });
});

// Auto-save functionality (optional)
let autoSaveTimeout;

function scheduleAutoSave() {
    clearTimeout(autoSaveTimeout);
    autoSaveTimeout = setTimeout(() => {
        // Auto-save implementation
        console.log('Auto-saving grades...');
    }, 5000); // Save after 5 seconds of inactivity
}

// Add event listeners to all grade inputs for auto-save
document.addEventListener('DOMContentLoaded', function() {
    const gradeInputs = document.querySelectorAll('.grade-input');
    
    gradeInputs.forEach(input => {
        input.addEventListener('input', scheduleAutoSave);
    });
});

// Validation functions
function validateGradeInput(input) {
    const value = parseFloat(input.value);
    
    if (isNaN(value) || value < 0 || value > 100) {
        input.style.borderColor = '#dc3545';
        return false;
    } else {
        input.style.borderColor = '#28a745';
        return true;
    }
}

// Add validation to all grade inputs
document.addEventListener('DOMContentLoaded', function() {
    const gradeInputs = document.querySelectorAll('.grade-input');
    
    gradeInputs.forEach(input => {
        input.addEventListener('blur', function() {
            validateGradeInput(this);
        });
        
        input.addEventListener('input', function() {
            // Reset border color while typing
            this.style.borderColor = '#e9ecef';
        });
    });
});

// Keyboard shortcuts
document.addEventListener('keydown', function(e) {
    // Ctrl+S to save
    if (e.ctrlKey && e.key === 's') {
        e.preventDefault();
        const saveButton = document.getElementById('btnSaveGrades');
        if (saveButton) {
            saveButton.click();
        }
    }
    
    // Tab navigation enhancement
    if (e.key === 'Tab') {
        const activeElement = document.activeElement;
        if (activeElement && activeElement.classList.contains('grade-input')) {
            // Custom tab behavior for grade inputs if needed
        }
    }
});