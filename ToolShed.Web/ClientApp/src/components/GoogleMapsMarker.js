import React from 'react';
import '../Styles/GoogleMapsMarker.css';
import { Motion } from 'react-motion';

export const GoogleMapsMarker = ({
    styles,
    defaultMotionStyle, motionStyle,
}) => (
        <Motion
            defaultStyle={defaultMotionStyle}
            style={motionStyle}
        >
            {
                ({ scale }) => (
                    <div
                        className={styles.marker}
                        style={{
                            transform: `translate3D(0,0,0) scale(${scale}, ${scale})`,
                        }}
                    >
                    </div>
                )
            }
        </Motion>
);