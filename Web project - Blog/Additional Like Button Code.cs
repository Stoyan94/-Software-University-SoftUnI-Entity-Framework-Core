< div class= "card" >
    < div class= "card-body" >
    < h5 class= "card-title" > @article.Title </ h5 >
    < p class= "card-text" > @article.Content </ p >
    < button id = "likeButton_@article.Id" class= "btn btn-primary" > Like </ button >
    </ div >
    </ div >

    < script >
    document.addEventListener('DOMContentLoaded', function() {
    // Add event listener to all like buttons
    const likeButtons = document.querySelectorAll('button[id^="likeButton_"]');

    likeButtons.forEach(button => {
        button.addEventListener('click', () => {
            button.classList.toggle('liked');
            button.textContent = button.classList.contains('liked') ? 'Liked' : 'Like';
            button.classList.contains('liked')
                ? button.classList.replace('btn-primary', 'btn-success')
                : button.classList.replace('btn-success', 'btn-primary');
        });
    });
});
</ script >