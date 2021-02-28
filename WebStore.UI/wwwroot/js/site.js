/*----public website navbar & set current slide on 0 index carousel slide------------------------------*/
document.addEventListener('DOMContentLoaded', () => {
    const track = document.querySelector('.carousel__track');
    if (!track) return;
    else {
        const slides = Array.from(track.children);
        slides[0].classList.add('current-slide');
    }
    // Get all "navbar-burger" elements
    const $navbarBurgers = Array.prototype.slice.call(document.querySelectorAll('.navbar-burger'), 0);
    // Check if there are any navbar burgers
    if ($navbarBurgers.length > 0) {
        // Add a click event on each of them
        $navbarBurgers.forEach(el => {
            el.addEventListener('click', () => {
                // Get the target from the "data-target" attribute
                const target = el.dataset.target;
                const $target = document.getElementById(target);
                // Toggle the "is-active" class on both the "navbar-burger" and the "navbar-menu"
                el.classList.toggle('is-active');
                $target.classList.toggle('is-active');
            });
        });
    }
});

/*-----------------------home page carousel------------------------------*/
document.addEventListener('DOMContentLoaded', () => {
    //getters
    const track = document.querySelector('.carousel__track');
    if (!track) return;
    else {
        const slides = Array.from(track.children);
        const prevButton = document.querySelector('.carousel__button--left');
        const nextButton = document.querySelector('.carousel__button--right');
        const indicatorNav = document.querySelector('.carousel__nav');
        const indicators = Array.from(indicatorNav.children);
        const slideWidth = slides[0].getBoundingClientRect().width;
        //arrange slides side by side
        const setSlidePositions = (slide, index) => {
            slide.style.left = slideWidth * index + 'px';
        };
        slides.forEach(setSlidePositions);
        //move slide function
        const moveSlide = (track, currentSlide, targetSlide) => {
            track.style.transform = 'translateX(-' + targetSlide.style.left + ')';
            currentSlide.classList.remove('current-slide');
            targetSlide.classList.add('current-slide');
        };
        //update indicator function
        const updateIndicators = (currentIndicator, targetIndicator) => {
            currentIndicator.classList.remove('current-slide');
            targetIndicator.classList.add('current-slide');
        };
        //hide or show chevron based on index
        const hideShowChevrons = (targetIndex, prevButton, nextButton, slides) => {
            if (targetIndex === 0) {
                prevButton.classList.add('hide-chevron');
                nextButton.classList.remove('hide-chevron');
            } else if (targetIndex === slides.length - 1) {
                prevButton.classList.remove('hide-chevron');
                nextButton.classList.add('hide-chevron');
            } else {
                prevButton.classList.remove('hide-chevron');
                nextButton.classList.remove('hide-chervon');
            }
        };
        //left button action
        prevButton.addEventListener('click', e => {
            //find current and prev slide (going left) & get indicators
            const currentSlide = track.querySelector('.current-slide');
            const prevSlide = currentSlide.previousElementSibling;
            const currentIndicator = indicatorNav.querySelector('.current-slide');
            const prevIndicator = currentIndicator.previousElementSibling;
            const prevIndex = slides.findIndex(slide => slide === prevSlide);
            //if not null move the slide & update indicators
            if (!prevSlide) return;
            else {
                moveSlide(track, currentSlide, prevSlide);
                updateIndicators(currentIndicator, prevIndicator);
                hideShowChevrons(prevIndex, prevButton, nextButton, slides)
            }
        });
        //right button action
        nextButton.addEventListener('click', e => {
            //find current and next slide (going right) & get indicators
            const currentSlide = track.querySelector('.current-slide');
            const nextSlide = currentSlide.nextElementSibling;
            const currentIndicator = indicatorNav.querySelector('.current-slide');
            const nextIndicator = currentIndicator.nextElementSibling;
            const nextIndex = slides.findIndex(slide => slide === nextSlide);
            //if not null move the slide & update indicators
            if (!nextSlide) return;
            else {
                moveSlide(track, currentSlide, nextSlide);
                updateIndicators(currentIndicator, nextIndicator);
                hideShowChevrons(nextIndex, prevButton, nextButton, slides)
            }
        });
        //indicator action
        indicatorNav.addEventListener('click', e => {
            //find the indicators of current slide & indicators
            const targetIndicator = e.target.closest('button');
            if (!targetIndicator) return;
            const currentSlide = track.querySelector('.current-slide');
            const currentIndicator = indicatorNav.querySelector('.current-slide');
            //get index of array of indicators
            const targetIndex = indicators.findIndex(indi => indi === targetIndicator)
            const targetSlide = slides[targetIndex];
            //move to slide & change indicator colour
            moveSlide(track, currentSlide, targetSlide);
            updateIndicators(currentIndicator, targetIndicator);
            //add or remove hide-chevron 
            hideShowChevrons(targetIndex, prevButton, nextButton, slides);
        });
    }
});
