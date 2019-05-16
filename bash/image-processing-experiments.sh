#!/bin/bash
magick identify -format "Using image file: %f, width %w, height %h\n" "${1}"
magick convert "${1}" -blur 0x5 - | magick montage -geometry +1+1 "${1}" - - | magick display&
magick convert "${1}"  -colorspace gray "gray-${1}"
