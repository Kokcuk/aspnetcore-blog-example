/// <binding />
/*
This file in the main entry point for defining Gulp tasks and using Gulp plugins.
Click here to learn more. http://go.microsoft.com/fwlink/?LinkId=518007
*/

var gulp = require('gulp');
var rename = require('gulp-rename');

gulp.task('default', function () {
    gulp.src('node_modules/bootstrap/dist/**/*.css')
    .pipe(rename({ dirname: '' }))
    .pipe(gulp.dest('wwwroot/css'));

    gulp.src('node_modules/bootstrap/dist/**/*.js')
    .pipe(rename({ dirname: '' }))
    .pipe(gulp.dest('wwwroot/js'));

    gulp.src('node_modules/jquery/dist/**/*.js')
    .pipe(rename({ dirname: '' }))
    .pipe(gulp.dest('wwwroot/js'));

    gulp.src('node_modules/font-awesome/fonts/*')
    .pipe(rename({ dirname: '' }))
    .pipe(gulp.dest('wwwroot/fonts'));

    gulp.src('node_modules/font-awesome/css/*.min.css')
    .pipe(rename({ dirname: '' }))
    .pipe(gulp.dest('wwwroot/css'));
});